/*
*  Warewolf - Once bitten, there's no going back
*  Copyright 2018 by Warewolf Ltd <alpha@warewolf.io>
*  Licensed under GNU Affero General Public License 3.0 or later. 
*  Some rights reserved.
*  Visit our website for more information <http://warewolf.io/>
*  AUTHORS <http://warewolf.io/authors.php> , CONTRIBUTORS <http://warewolf.io/contributors.php>
*  @license GNU Affero General Public License <http://www.gnu.org/licenses/agpl-3.0.html>
*/

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using System.Xml;
using Dev2.Data.Interfaces;
using Dev2.Studio.Interfaces;
using Dev2.Studio.ViewModels.Workflow;
using Dev2.UI;
using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Folding;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Indentation;
using Infragistics.Controls.Grids;
using Infragistics.Controls.Grids.Primitives;
using Newtonsoft.Json;
using Warewolf.Studio.Core;


namespace Dev2.Studio.Views.Workflow
{
    /// <summary>
    /// Interaction logic for WorkflowInputDataWindow.xaml
    /// </summary>
    public partial class WorkflowInputDataView
    {
        public WorkflowInputDataView()
        {
            InitializeComponent();
            SetUpTextEditor();
            PopupViewManageEffects.AddBlackOutEffect(_blackoutGrid);
            _currentTab = InputTab.Grid;
        }

        TextEditor _editor;
        TextEditor _jsonEditor;
        AbstractFoldingStrategy _foldingStrategy;
        FoldingManager _foldingManager;
        DispatcherTimer _foldingUpdateTimer;
        readonly Grid _blackoutGrid = new Grid();
        InputTab _currentTab;

        void SetUpTextEditor()
        {
            _editor = new TextEditor { SyntaxHighlighting = HighlightingManager.Instance.GetDefinition("XML"), ShowLineNumbers = true, VerticalScrollBarVisibility = ScrollBarVisibility.Auto, HorizontalScrollBarVisibility = ScrollBarVisibility.Auto };
            _editor.SetValue(AutomationProperties.AutomationIdProperty, "UI_XMLEditor_AutoID");

            _jsonEditor = new TextEditor { SyntaxHighlighting = HighlightingManager.Instance.GetDefinition("JavaScript"), ShowLineNumbers = true, VerticalScrollBarVisibility = ScrollBarVisibility.Auto, HorizontalScrollBarVisibility = ScrollBarVisibility.Auto };
            _jsonEditor.SetValue(AutomationProperties.AutomationIdProperty, "UI_JsonEditor_AutoID");

            _foldingStrategy = new XmlFoldingStrategy();
            _foldingManager = FoldingManager.Install(_editor.TextArea);
            _editor.TextArea.IndentationStrategy = new DefaultIndentationStrategy();
            _jsonEditor.TextArea.IndentationStrategy = new DefaultIndentationStrategy();
            _foldingUpdateTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(2) };
            _foldingUpdateTimer.Tick += OnFoldingUpdateTimerOnTick;
            _foldingUpdateTimer.Start();
        }

        void OnFoldingUpdateTimerOnTick(object sender, EventArgs e)
        {
            if (_foldingStrategy != null && _foldingManager != null && !string.IsNullOrEmpty(_editor.Document.Text))
            {
                _foldingStrategy.UpdateFoldings(_foldingManager, _editor.Document);
            }

        }

        void ShowDataInOutputWindow(string input)
        {
            _editor.Text = input;
            XmlOutput.Content = _editor;
        }

        void TryShowDataInOutputWindow(WorkflowInputDataViewModel vm)
        {
            if (_currentTab == InputTab.Grid)
            {
                try
                {
                    vm.SetXmlData();
                    ShowDataInOutputWindow(vm.XmlData);
                }
                catch
                {
                    vm.ShowInvalidDataPopupMessage();
                }
            }
            if (_currentTab == InputTab.Json)
            {
                try
                {
                    vm.XmlData = GetXmlDataFromJson();
                    vm.SetWorkflowInputData();
                    vm.SetXmlData();
                    ShowDataInOutputWindow(vm.XmlData);
                }
                catch
                {
                    vm.ShowInvalidDataPopupMessage();
                }
            }
        }

        void TextBoxTextChanged(object sender, RoutedEventArgs routedEventArgs)
        {
            if (routedEventArgs.OriginalSource is IntellisenseTextBox tb)
            {
                var dli = tb.DataContext as IDataListItem;
                var vm = DataContext as WorkflowInputDataViewModel;
                vm?.AddRow(dli);
            }
        }

        void TryChangeTab(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl ctrl)
            {
                var tabCtrl = ctrl;
                var tabItem = tabCtrl.SelectedItem as TabItem;
                if (DataContext is WorkflowInputDataViewModel vm)
                {
                    vm.IsInError = false;
                    if (tabItem != null && tabItem.Header.ToString() == "XML")
                    {
                        TryShowDataInOutputWindow(vm);
                        _currentTab = InputTab.Xml;
                    }
                    else if (tabItem != null && tabItem.Header.ToString() == "JSON")
                    {
                        SetCurrentTabToJson(vm);
                    }
                    else
                    {
                        TryChangeTab(vm);
                    }
                }
            }
        }

        void TryChangeTab(WorkflowInputDataViewModel vm)
        {
            try
            {
                var xmlData = _editor.Text;
                if (_currentTab == InputTab.Json)
                {
                    xmlData = GetXmlDataFromJson();
                }
                vm.XmlData = xmlData;
                vm.SetWorkflowInputData();
                _currentTab = InputTab.Grid;
            }
            catch (Exception ex)
            {
                vm.IsInError = true;
            }
        }

        private void SetCurrentTabToJson(WorkflowInputDataViewModel vm)
        {
            var xml = new XmlDocument();
            if (_currentTab == InputTab.Grid)
            {
                vm.SetXmlData();
                if (vm.XmlData != null)
                {
                    xml.LoadXml(vm.XmlData);
                }
            }
            if (_currentTab == InputTab.Xml && !string.IsNullOrEmpty(_editor.Text))
            {
                try
                {
                    xml.LoadXml(_editor.Text);
                }
                catch (Exception ex)
                {
                    vm.ShowInvalidDataPopupMessage();
                }
            }

            if (!string.IsNullOrEmpty(vm.JsonData))
            {
                _jsonEditor.Text = vm.JsonData;
            }
            else
            {
                if (xml.FirstChild != null)
                {
                    var json = JsonConvert.SerializeXmlNode(xml.FirstChild, Newtonsoft.Json.Formatting.Indented, true);
                    _jsonEditor.Text = json;
                }
            }
            JsonOutput.Content = _jsonEditor;
            _currentTab = InputTab.Json;
        }

        string GetXmlDataFromJson()
        {
            try
            {
                var xmlDocument = JsonConvert.DeserializeXmlNode(_jsonEditor.Text == "\"\"" ? "" : _jsonEditor.Text, "DataList",true);
                return xmlDocument == null ? String.Empty : xmlDocument.InnerXml;
            }
            catch (Exception)
            {
                var vm = DataContext as WorkflowInputDataViewModel;
                vm?.ShowInvalidDataPopupMessage();
            }
            return _editor.Text;
        }

        void MenuItemAddRow(object sender, RoutedEventArgs e)
        {

            if (DataContext is WorkflowInputDataViewModel vm && vm.AddBlankRow(DataListInputs.ActiveItem as IDataListItem, out int indexToSelect))
            {
                DataListInputs.ActiveItem = indexToSelect;
                Dispatcher.BeginInvoke(new Action(FocusOnAddition), DispatcherPriority.ApplicationIdle);
            }
        }

        void FocusOnAddition()
        {
            try
            {
                var row = GetSelectedRow(DataListInputs);
                if (row != null)
                {
                    var intelbox = FindByName("txtValue", row) as IntellisenseTextBox;
                    intelbox?.Focus();
                }
            }
            catch (Exception)
            {
                //
            }
        }

        void MenuItemDeleteRow(object sender, RoutedEventArgs e)
        {
            if (DataContext is WorkflowInputDataViewModel vm && vm.RemoveRow(DataListInputs.ActiveItem as IDataListItem, out int indexToSelect))
            {
                DataListInputs.ActiveItem = indexToSelect;
            }
        }

        void IntellisenseTextBoxPreviewKeyDown(object sender, KeyEventArgs e)
        {
            var vm = DataContext as WorkflowInputDataViewModel;

            if (e.KeyboardDevice.Modifiers == ModifierKeys.Shift && e.KeyboardDevice.IsKeyDown(Key.Insert))
            {
                InsertEmptyRow();
                e.Handled = true;
            }
            else
            {
                if (e.KeyboardDevice.Modifiers == ModifierKeys.Shift && e.Key == Key.Delete)
                {
                    DeleteLastRow();
                    e.Handled = true;
                }
            }
            if ((e.KeyboardDevice.Modifiers == ModifierKeys.Shift && (e.KeyboardDevice.IsKeyDown(Key.Tab) || e.Key == Key.Tab)) || e.KeyboardDevice.IsKeyDown(Key.Up))
            {
                MoveToPreviousRow(vm);
                e.Handled = true;
            }
            else
            {
                if (e.KeyboardDevice.IsKeyDown(Key.Tab) || e.KeyboardDevice.IsKeyDown(Key.Down))
                {
                    MoveToNextRow(vm);
                    e.Handled = true;
                }
            }
        }

        void MoveToNextRow(WorkflowInputDataViewModel vm)
        {
            var itemToSelect = vm?.GetNextRow(DataListInputs.ActiveItem as IDataListItem);
            if(itemToSelect != null)
            {
                DataListInputs.ActiveItem = itemToSelect;
                FocusOnAddition();
            }
        }

        void MoveToPreviousRow(WorkflowInputDataViewModel vm)
        {
            var itemToSelect = vm?.GetPreviousRow(DataListInputs.ActiveItem as IDataListItem);
            if(itemToSelect != null)
            {
                DataListInputs.ActiveItem = itemToSelect;
                FocusOnAddition();
            }
        }

        void GridPreviewKeyDown(object sender, KeyEventArgs e)
        {
            UIElement keyboardFocus = Keyboard.FocusedElement as TextBox;
            if (e.KeyboardDevice.IsKeyDown(Key.LeftShift) && e.KeyboardDevice.IsKeyDown(Key.Tab))
            {
                keyboardFocus?.MoveFocus(new TraversalRequest(FocusNavigationDirection.Previous));
            }
            if (e.KeyboardDevice.IsKeyDown(Key.Tab))
            {
                var vm = DataContext as WorkflowInputDataViewModel;
                var itemToSelect = vm?.GetNextRow(DataListInputs.ActiveItem as IDataListItem);
                if (itemToSelect != null)
                {
                    DataListInputs.ActiveItem = itemToSelect;
                    FocusOnAddition();
                }
            }
        }

        static FrameworkElement FindByName(string name, FrameworkElement root)
        {
            if (root != null)
            {
                var tree = new Stack<FrameworkElement>();
                tree.Push(root);
                while (tree.Count > 0)
                {
                    var current = tree.Pop();
                    if (current.Name == name)
                    {
                        return current;
                    }

                    var count = VisualTreeHelper.GetChildrenCount(current);
                    for (var supplierCounter = 0; supplierCounter < count; ++supplierCounter)
                    {
                        tree = TreePushChild(tree, current, supplierCounter);
                    }
                }
            }
            return null;
        }

        static Stack<FrameworkElement> TreePushChild(Stack<FrameworkElement> tree, FrameworkElement current, int supplierCounter)
        {
            var child = VisualTreeHelper.GetChild(current, supplierCounter);
            if (child is FrameworkElement item)
            {
                tree.Push(item);
            }
            return tree;
        }

        static CellsPanel GetSelectedRow(XamGrid grid)
        {
            var row = grid.ActiveCell?.Row;
            return row?.Control;
        }

        void ExecuteClicked(object sender, RoutedEventArgs e)
        {
            var tabItem = TabItems.SelectedItem as TabItem;
            if (DataContext is WorkflowInputDataViewModel vm)
            {
                vm.IsInError = false;
                if (tabItem != null)
                {
                    TrySetWorkflowInputData(tabItem, vm);
                }
            }
            DestroyTimer();
        }

        void TrySetWorkflowInputData(TabItem tabItem, WorkflowInputDataViewModel vm)
        {
            if (tabItem.Header.ToString() == "XML")
            {
                try
                {
                    vm.XmlData = _editor.Text;
                    vm.SetWorkflowInputData();
                }
                catch (Exception ex)
                {
                    vm.IsInError = true;
                }
            }
            else
            {
                if (tabItem.Header.ToString() == "JSON")
                {
                    vm.XmlData = GetXmlDataFromJson();
                    vm.SetWorkflowInputData();
                }
            }
        }

        void DestroyTimer()
        {
            if (_foldingUpdateTimer != null)
            {
                _foldingUpdateTimer.Tick -= OnFoldingUpdateTimerOnTick;
                _foldingUpdateTimer.Stop();
                _foldingUpdateTimer = null;
            }
        }

        void CancelClicked(object sender, RoutedEventArgs e)
        {
            DestroyTimer();
        }

        void WorkflowInputDataView_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        void WorkflowInputDataView_OnClosed(object sender, EventArgs e)
        {
            PopupViewManageEffects.RemoveBlackOutEffect(_blackoutGrid);
        }

        void WorkflowInputDataView_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
        }

        void InsertEmptyRow()
        {
            if (DataContext is WorkflowInputDataViewModel vm && vm.AddBlankRow(DataListInputs.ActiveItem as IDataListItem, out int indexToSelect))
            {
                DataListInputs.ActiveItem = indexToSelect;
                Dispatcher.BeginInvoke(new Action(FocusOnAddition), DispatcherPriority.ApplicationIdle);
            }
        }

        void DeleteLastRow()
        {
            if (DataContext is WorkflowInputDataViewModel vm && vm.RemoveRow(DataListInputs.ActiveItem as IDataListItem, out int indexToSelect))
            {
                DataListInputs.ActiveItem = indexToSelect;
            }
        }

        void DataListInputs_OnLoaded(object sender, RoutedEventArgs e)
        {
            if(DataListInputs?.Rows != null && DataListInputs.Rows.Count > 0)
            {
                var cellBaseCollection = DataListInputs.Rows[0].Cells;
                if(cellBaseCollection != null)
                {
                    var selectedCell = (Cell)cellBaseCollection[1];
                    DataListInputs.ActiveCell = selectedCell;
                }
            }
            FocusOnAddition();
        }

        void WorkflowInputDataView_OnKeyUp(object sender, KeyEventArgs e)
        {
            if ((Keyboard.Modifiers == (ModifierKeys.Alt | ModifierKeys.Control)) && (e.Key == Key.F4))
            {
                var mainViewModel = CustomContainer.Get<IShellViewModel>();
                mainViewModel?.ResetMainView();
            }
        }
    }

    public enum InputTab
    {
        Grid,
        Xml,
        Json
    }
}
