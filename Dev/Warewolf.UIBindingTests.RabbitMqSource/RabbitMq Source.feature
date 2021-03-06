﻿@RabbitMqSource
@MSTest:DeploymentItem:InfragisticsWPF4.Controls.Interactions.XamDialogWindow.v15.1.dll
@MSTest:DeploymentItem:InfragisticsWPF4.Controls.Grids.XamGrid.v15.1.dll
@MSTest:DeploymentItem:InfragisticsWPF4.DataPresenter.v15.1.dll
@MSTest:DeploymentItem:Warewolf_Studio.exe
@MSTest:DeploymentItem:Newtonsoft.Json.dll
@MSTest:DeploymentItem:Microsoft.Practices.Prism.SharedInterfaces.dll
@MSTest:DeploymentItem:System.Windows.Interactivity.dll
@MSTest:DeploymentItem:Warewolf.Studio.Themes.Luna.dll
@MSTest:DeploymentItem:EnableDocker.txt
Feature: RabbitMq Source
	In order to share settings
	I want to save my RabbitMq source Settings
	So that I can reuse them

Scenario: Create New RabbitMq source
	Given I open New RabbitMq Source
	Then "New RabbitMQ Source" tab is opened
	And the title is "New RabbitMQ Source"
	And "Host" input is ""
	And "Port" input is "5672"
	And "User Name" input is ""
	And "Password" input is ""
	And "Virtual Host" input is "/"
	And "Test Connection" is "Disabled"
	And "Save" is "Disabled"

Scenario: Enable Send and Enable Save
	Given I open New RabbitMq Source
	Then "New RabbitMQ Source" tab is opened
	And I type Host as "test-rabbitmq"
	And "Port" input is "5672"
	And I type Username as "test"
	And I type Password as "test"
	And "Test Connection" is "Enabled"
	And "Save" is "Disabled"
	When I click "Test Connection"
	And Send is "Successful"
	When I save as "TestRabbitMq"
	And the save dialog is opened

Scenario: Fail Send Shows correct error message
	Given I open New RabbitMq Source
	Then "New RabbitMQ Source" tab is opened
	And I type Host as "test"
	And "Port" input is "5672"
	And I type Username as "test"
	And I type Password as "test"
	And "Test Connection" is "Enabled"
	And "Save" is "Disabled"
	And Send is "Unsuccessful"
	Then Send is "Failed: None of the specified endpoints were reachable"
	And "Save" is "Disabled"
	And the error message is "Failed: None of the specified endpoints were reachable"