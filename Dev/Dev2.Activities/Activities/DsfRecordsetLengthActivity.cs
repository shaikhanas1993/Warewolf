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
using System.Activities;
using System.Collections.Generic;
using Dev2.Activities;
using Dev2.Activities.Debug;
using Dev2.Common.Interfaces.Diagnostics.Debug;
using Dev2.Common.State;
using Dev2.Data.TO;
using Dev2.Data.Util;
using Dev2.Diagnostics;
using Dev2.Interfaces;
using Dev2.Util;
using Dev2.Validation;
using Unlimited.Applications.BusinessDesignStudio.Activities.Utilities;
using Warewolf.Resource.Errors;
using Warewolf.Storage.Interfaces;


namespace Unlimited.Applications.BusinessDesignStudio.Activities

{

    public class DsfRecordsetLengthActivity : DsfActivityAbstract<string>,IEquatable<DsfRecordsetLengthActivity>
    {
        #region Fields

        #endregion

        /// <summary>
        /// Gets or sets the name of the recordset.
        /// </summary>  
        [Inputs("RecordsetName"), FindMissing]
        public string RecordsetName { get; set; }

        /// <summary>
        /// Gets or sets the count number.
        /// </summary>  
        [Outputs("Length"), FindMissing]
        public string RecordsLength { get; set; }

        public DsfRecordsetLengthActivity()
            : base("Length")
        {
            RecordsetName = string.Empty;
            RecordsLength = string.Empty;
            DisplayName = "Length";
        }

        public override IEnumerable<StateVariable> GetState()
        {
            return new[] {
                new StateVariable
                {
                    Name = "RecordsetName",
                    Value = RecordsetName,
                    Type = StateVariable.StateType.Input
                },
                new StateVariable
                {
                    Name="RecordsLength",
                    Value = RecordsLength,
                    Type = StateVariable.StateType.Output
                }
            };
        }

        public override List<string> GetOutputs() => new List<string> { RecordsLength };

        protected override void CacheMetadata(NativeActivityMetadata metadata)
        {
            base.CacheMetadata(metadata);

        }
        

        protected override void OnExecute(NativeActivityContext context)
        {
            var dataObject = context.GetExtension<IDSFDataObject>();
            ExecuteTool(dataObject, 0);
        }

        protected override void ExecuteTool(IDSFDataObject dataObject, int update)
        {

            var allErrors = new ErrorResultTO();
            var errors = new ErrorResultTO();
            allErrors.MergeErrors(errors);
            InitializeDebug(dataObject);
            // Process if no errors
            try
            {
                ValidateRecordsetName(RecordsetName, errors);
                allErrors.MergeErrors(errors);
                if(!allErrors.HasErrors())
                {
                    try
                    {
                        allErrors = TryExecute(dataObject, update, allErrors);
                    }
                    catch (Exception e)
                    {
                        allErrors.AddError(e.StackTrace);
                        dataObject.Environment.Assign(RecordsLength, "0", update);
                        AddDebugOutputItem(new DebugItemStaticDataParams("0", RecordsLength, "", "="));
                    }
                }
            }
            finally
            {
                // Handle Errors
                var hasErrors = allErrors.HasErrors();
                if(hasErrors)
                {
                    DisplayAndWriteError("DsfRecordsetLengthActivity", allErrors);
                    var errorString = allErrors.MakeDisplayReady();
                    dataObject.Environment.AddError(errorString);
                }
                if(dataObject.IsDebugMode())
                {
                    DispatchDebugState(dataObject, StateType.Before, update);
                    DispatchDebugState(dataObject, StateType.After, update);
                }
            }
        }

        ErrorResultTO TryExecute(IDSFDataObject dataObject, int update, ErrorResultTO allErrors)
        {
            var rs = DataListUtil.ExtractRecordsetNameFromValue(RecordsetName);
            if (RecordsLength == string.Empty)
            {
                allErrors.AddError(ErrorResource.BlankResultVariable);
            }
            if (dataObject.IsDebugMode())
            {
                var warewolfEvalResult = dataObject.Environment.Eval(RecordsetName.Replace("()", "(*)"), update);
                if (warewolfEvalResult.IsWarewolfRecordSetResult && warewolfEvalResult is CommonFunctions.WarewolfEvalResult.WarewolfRecordSetResult recsetResult)
                {
                    AddDebugInputItem(new DebugItemWarewolfRecordset(recsetResult.Item, RecordsetName, "Recordset", "="));
                }

                if (warewolfEvalResult.IsWarewolfAtomListresult && warewolfEvalResult is CommonFunctions.WarewolfEvalResult.WarewolfAtomListresult atomListResult)
                {
                    AddDebugInputItem(new DebugEvalResult(RecordsetName, "Recordset", dataObject.Environment, update));
                }

            }
            var rule = new IsSingleValueRule(() => RecordsLength);
            var single = rule.Check();
            if (single != null)
            {
                allErrors.AddError(single.Message);
            }
            else
            {
                var count = 0;
                if (dataObject.Environment.HasRecordSet(RecordsetName))
                {
                    count = dataObject.Environment.GetLength(rs);
                }
                else
                {
                    allErrors.AddError("Recordset: " + RecordsetName + " does not exist.");
                }
                var value = count.ToString();
                dataObject.Environment.Assign(RecordsLength, value, update);
                AddDebugOutputItem(new DebugItemWarewolfAtomResult(value, RecordsLength, ""));
            }
            return allErrors;
        }

        #region Get Debug Inputs/Outputs

        #region GetDebugInputs

        public override List<DebugItem> GetDebugInputs(IExecutionEnvironment env, int update)
        {
            foreach(IDebugItem debugInput in _debugInputs)
            {
                debugInput.FlushStringBuilder();
            }
            return _debugInputs;
        }

        #endregion

        #region GetDebugOutputs

        public override List<DebugItem> GetDebugOutputs(IExecutionEnvironment env, int update)
        {
            foreach(IDebugItem debugOutput in _debugOutputs)
            {
                debugOutput.FlushStringBuilder();
            }
            return _debugOutputs;
        }

        #endregion


        #endregion Get Inputs/Outputs

        public override void UpdateForEachInputs(IList<Tuple<string, string>> updates)
        {
            if(updates != null && updates.Count == 1)
            {
                RecordsetName = updates[0].Item2;
            }
        }

        public override void UpdateForEachOutputs(IList<Tuple<string, string>> updates)
        {
            if(updates != null && updates.Count == 1)
            {
                RecordsLength = updates[0].Item2;
            }
        }

        #region GetForEachInputs/Outputs

        public override IList<DsfForEachItem> GetForEachInputs() => GetForEachItems(RecordsetName);

        public override IList<DsfForEachItem> GetForEachOutputs() => GetForEachItems(RecordsLength);

        #endregion

        public bool Equals(DsfRecordsetLengthActivity other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return base.Equals(other) && string.Equals(RecordsetName, other.RecordsetName) && string.Equals(RecordsLength, other.RecordsLength);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return Equals((DsfRecordsetLengthActivity) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = base.GetHashCode();
                hashCode = (hashCode * 397) ^ (RecordsetName != null ? RecordsetName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (RecordsLength != null ? RecordsLength.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}
