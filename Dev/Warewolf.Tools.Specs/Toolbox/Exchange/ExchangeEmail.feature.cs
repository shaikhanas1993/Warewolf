﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:2.3.2.0
//      SpecFlow Generator Version:2.3.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace Warewolf.Tools.Specs.Toolbox.Exchange
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "2.3.2.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [Microsoft.VisualStudio.TestTools.UnitTesting.TestClassAttribute()]
    public partial class ExchangeEmailFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private Microsoft.VisualStudio.TestTools.UnitTesting.TestContext _testContext;
        
#line 1 "ExchangeEmail.feature"
#line hidden
        
        public virtual Microsoft.VisualStudio.TestTools.UnitTesting.TestContext TestContext
        {
            get
            {
                return this._testContext;
            }
            set
            {
                this._testContext = value;
            }
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.ClassInitializeAttribute()]
        public static void FeatureSetup(Microsoft.VisualStudio.TestTools.UnitTesting.TestContext testContext)
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner(null, 0);
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "ExchangeEmail", "\tIn order to avoid silly mistakes\r\n\tAs a math idiot\r\n\tI want to be told the sum o" +
                    "f two numbers", ProgrammingLanguage.CSharp, new string[] {
                        "Exchange"});
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.ClassCleanupAttribute()]
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestInitializeAttribute()]
        public virtual void TestInitialize()
        {
            if (((testRunner.FeatureContext != null) 
                        && (testRunner.FeatureContext.FeatureInfo.Title != "ExchangeEmail")))
            {
                global::Warewolf.Tools.Specs.Toolbox.Exchange.ExchangeEmailFeature.FeatureSetup(null);
            }
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCleanupAttribute()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<Microsoft.VisualStudio.TestTools.UnitTesting.TestContext>(TestContext);
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Send New Exchange Email to multiple recipients")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "ExchangeEmail")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("Exchange")]
        public virtual void SendNewExchangeEmailToMultipleRecipients()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Send New Exchange Email to multiple recipients", ((string[])(null)));
#line 7
this.ScenarioSetup(scenarioInfo);
#line 8
 testRunner.Given("I have a new exchange email variable \"[[firstMail]]\" equal to \"testmail@freemail." +
                    "com\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 9
 testRunner.And("I have a new exchange email variable \"[[secondMail]]\" equal to \"test2@freemail.co" +
                    "m\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 10
 testRunner.And("new to exchange address is \"[[firstMail]];[[secondMail]]\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 11
 testRunner.And("the new exchange subject is \"Just testing\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 12
 testRunner.And("new exchange body is \"testing email from the cool specflow\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 13
 testRunner.When("the new exchange email tool is executed", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 14
 testRunner.Then("the new exchange email result will be \"Success\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 15
 testRunner.And("the new exchange execution has \"NO\" error", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                        "To",
                        "Subject",
                        "Body"});
            table1.AddRow(new string[] {
                        "[[firstMail]];[[secondMail]] = testmail@freemail.com;test2@freemail.com",
                        "Just testing",
                        "testing email from the cool specflow"});
#line 16
 testRunner.And("the debug inputs as", ((string)(null)), table1, "And ");
#line hidden
            TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                        ""});
            table2.AddRow(new string[] {
                        "[[result]] = Success"});
#line 19
 testRunner.And("the debug output as", ((string)(null)), table2, "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Send New Exchange email with no To Accounts")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "ExchangeEmail")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("Exchange")]
        public virtual void SendNewExchangeEmailWithNoToAccounts()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Send New Exchange email with no To Accounts", ((string[])(null)));
#line 23
this.ScenarioSetup(scenarioInfo);
#line 24
 testRunner.Given("new exchange to address is \"\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 25
 testRunner.And("the new exchange subject is \"Just testing\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 26
 testRunner.And("new exchange body is \"testing email from the cool specflow\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 27
 testRunner.When("the new exchange email tool is executed \"\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 28
 testRunner.Then("the new exchange email result will be \"\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            TechTalk.SpecFlow.Table table3 = new TechTalk.SpecFlow.Table(new string[] {
                        "To",
                        "Subject",
                        "Body"});
            table3.AddRow(new string[] {
                        "\"\"",
                        "Just testing",
                        "testing email from the cool specflow"});
#line 29
 testRunner.And("the debug inputs as", ((string)(null)), table3, "And ");
#line hidden
            TechTalk.SpecFlow.Table table4 = new TechTalk.SpecFlow.Table(new string[] {
                        ""});
            table4.AddRow(new string[] {
                        "[[result]] ="});
#line 32
 testRunner.And("the debug output as", ((string)(null)), table4, "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Send New Exchange email with Subject as both text and variable as xml")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "ExchangeEmail")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("Exchange")]
        public virtual void SendNewExchangeEmailWithSubjectAsBothTextAndVariableAsXml()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Send New Exchange email with Subject as both text and variable as xml", ((string[])(null)));
#line 36
this.ScenarioSetup(scenarioInfo);
#line 37
 testRunner.Given("new exchange to address is \"test1@freemail.com\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 38
 testRunner.And("I have a new exchange email variable \"[[subject]]\" equal to \"<Wow>400%</Wow>\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 39
 testRunner.And("the new exchange subject is \"News: [[subject]]\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 40
 testRunner.And("new exchange body is \"testing email from the cool specflow\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 41
 testRunner.When("the new exchange email tool is executed", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 42
 testRunner.Then("the new exchange email result will be \"Success\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 43
 testRunner.And("the new exchange execution has \"NO\" error", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table5 = new TechTalk.SpecFlow.Table(new string[] {
                        "To",
                        "Subject",
                        "Body"});
            table5.AddRow(new string[] {
                        "test1@freemail.com",
                        "News: [[subject]] = News: <Wow>400%</Wow>",
                        "testing email from the cool specflow"});
#line 44
 testRunner.And("the debug inputs as", ((string)(null)), table5, "And ");
#line hidden
            TechTalk.SpecFlow.Table table6 = new TechTalk.SpecFlow.Table(new string[] {
                        ""});
            table6.AddRow(new string[] {
                        "[[result]] = Success"});
#line 47
 testRunner.And("the debug output as", ((string)(null)), table6, "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Send New Exchange email with no body")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "ExchangeEmail")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("Exchange")]
        public virtual void SendNewExchangeEmailWithNoBody()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Send New Exchange email with no body", ((string[])(null)));
#line 51
this.ScenarioSetup(scenarioInfo);
#line 52
 testRunner.Given("new exchange to address is \"test1@freemail.com\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 53
 testRunner.And("the new exchange subject is \"Testing this cool framework\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 54
 testRunner.When("the new exchange email tool is executed", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 55
 testRunner.Then("the new exchange email result will be \"Success\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 56
 testRunner.And("the new exchange execution has \"NO\" error", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table7 = new TechTalk.SpecFlow.Table(new string[] {
                        "To",
                        "Subject",
                        "Body"});
            table7.AddRow(new string[] {
                        "test1@freemail.com",
                        "Testing this cool framework",
                        "\"\""});
#line 57
 testRunner.And("the debug inputs as", ((string)(null)), table7, "And ");
#line hidden
            TechTalk.SpecFlow.Table table8 = new TechTalk.SpecFlow.Table(new string[] {
                        ""});
            table8.AddRow(new string[] {
                        "[[result]] = Success"});
#line 60
 testRunner.And("the debug output as", ((string)(null)), table8, "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Send New Exchange email with Body as both text and variable")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "ExchangeEmail")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("Exchange")]
        public virtual void SendNewExchangeEmailWithBodyAsBothTextAndVariable()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Send New Exchange email with Body as both text and variable", ((string[])(null)));
#line 64
this.ScenarioSetup(scenarioInfo);
#line 65
 testRunner.Given("new exchange to address is \"test1@freemail.com\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 66
 testRunner.And("I have a new exchange email variable \"[[body]]\" equal to \"<body><inner>inside</in" +
                    "ner></body>\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 67
 testRunner.And("the new exchange subject is \"News\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 68
 testRunner.And("new exchange body is \"testing email from [[body]] the cool specflow\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 69
 testRunner.When("the new exchange email tool is executed", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 70
 testRunner.Then("the new exchange email result will be \"Success\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 71
 testRunner.And("the new exchange execution has \"NO\" error", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table9 = new TechTalk.SpecFlow.Table(new string[] {
                        "To",
                        "Subject",
                        "Body"});
            table9.AddRow(new string[] {
                        "test1@freemail.com",
                        "News",
                        "testing email from [[body]] the cool specflow = testing email from <body><inner>i" +
                            "nside</inner></body> the cool specflow"});
#line 72
 testRunner.And("the debug inputs as", ((string)(null)), table9, "And ");
#line hidden
            TechTalk.SpecFlow.Table table10 = new TechTalk.SpecFlow.Table(new string[] {
                        ""});
            table10.AddRow(new string[] {
                        "[[result]] = Success"});
#line 75
 testRunner.And("the debug output as", ((string)(null)), table10, "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Send New Exchange email with variable as Body that is xml")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "ExchangeEmail")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("Exchange")]
        public virtual void SendNewExchangeEmailWithVariableAsBodyThatIsXml()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Send New Exchange email with variable as Body that is xml", ((string[])(null)));
#line 79
this.ScenarioSetup(scenarioInfo);
#line 80
 testRunner.Given("new exchange to address is \"test1@freemail.com\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 81
 testRunner.And("I have a new exchange email variable \"[[body]]\" equal to \"<body><inner>inside</in" +
                    "ner></body>\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 82
 testRunner.And("the new exchange subject is \"News\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 83
 testRunner.And("new exchange body is \"[[body]]\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 84
 testRunner.When("the new exchange email tool is executed", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 85
 testRunner.Then("the new exchange email result will be \"Success\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 86
 testRunner.And("the new exchange execution has \"NO\" error", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table11 = new TechTalk.SpecFlow.Table(new string[] {
                        "To",
                        "Subject",
                        "Body"});
            table11.AddRow(new string[] {
                        "test1@freemail.com",
                        "News",
                        "[[body]] =  <body><inner>inside</inner></body>"});
#line 87
 testRunner.And("the debug inputs as", ((string)(null)), table11, "And ");
#line hidden
            TechTalk.SpecFlow.Table table12 = new TechTalk.SpecFlow.Table(new string[] {
                        ""});
            table12.AddRow(new string[] {
                        "[[result]] = Success"});
#line 90
 testRunner.And("the debug output as", ((string)(null)), table12, "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Send New Exchange email with everything blank")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "ExchangeEmail")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("Exchange")]
        public virtual void SendNewExchangeEmailWithEverythingBlank()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Send New Exchange email with everything blank", ((string[])(null)));
#line 94
this.ScenarioSetup(scenarioInfo);
#line 95
 testRunner.When("the new exchange email tool is executed \"\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 96
 testRunner.Then("the new exchange email result will be \"\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            TechTalk.SpecFlow.Table table13 = new TechTalk.SpecFlow.Table(new string[] {
                        "To",
                        "Subject",
                        "Body"});
            table13.AddRow(new string[] {
                        "\"\"",
                        "\"\"",
                        "\"\""});
#line 97
 testRunner.And("the debug inputs as", ((string)(null)), table13, "And ");
#line hidden
            TechTalk.SpecFlow.Table table14 = new TechTalk.SpecFlow.Table(new string[] {
                        ""});
            table14.AddRow(new string[] {
                        "[[result]] ="});
#line 100
 testRunner.And("the debug output as", ((string)(null)), table14, "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Send New Exchange email with a negative index recordset for Recipients")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "ExchangeEmail")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("Exchange")]
        public virtual void SendNewExchangeEmailWithANegativeIndexRecordsetForRecipients()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Send New Exchange email with a negative index recordset for Recipients", ((string[])(null)));
#line 104
this.ScenarioSetup(scenarioInfo);
#line 105
 testRunner.And("new exchange to address is \"[[me(-1).to]]\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 106
 testRunner.And("the new exchange subject is \"Just testing\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 107
 testRunner.And("new exchange body is \"testing email from the cool specflow\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 108
 testRunner.When("the new exchange email tool is executed", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 109
 testRunner.Then("the new exchange email result will be \"\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 110
 testRunner.And("the new exchange execution has \"AN\" error", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table15 = new TechTalk.SpecFlow.Table(new string[] {
                        "To",
                        "Subject",
                        "Body"});
            table15.AddRow(new string[] {
                        "[[me(-1).to]] =",
                        "Just testing",
                        "testing email from the cool specflow"});
#line 111
 testRunner.And("the debug inputs as", ((string)(null)), table15, "And ");
#line hidden
            TechTalk.SpecFlow.Table table16 = new TechTalk.SpecFlow.Table(new string[] {
                        ""});
            table16.AddRow(new string[] {
                        "[[result]] ="});
#line 114
 testRunner.And("the debug output as", ((string)(null)), table16, "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Send New Exchange email with a negative index recordset for Subject")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "ExchangeEmail")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("Exchange")]
        public virtual void SendNewExchangeEmailWithANegativeIndexRecordsetForSubject()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Send New Exchange email with a negative index recordset for Subject", ((string[])(null)));
#line 118
this.ScenarioSetup(scenarioInfo);
#line 119
 testRunner.And("new exchange to address is \"test1@freemail.com\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 120
 testRunner.And("the new exchange subject is \"[[my(-1).subject]]\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 121
 testRunner.And("new exchange body is \"testing email from the cool specflow\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 122
 testRunner.When("the new exchange email tool is executed", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 123
 testRunner.Then("the new exchange email result will be \"\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 124
 testRunner.And("the new exchange execution has \"AN\" error", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table17 = new TechTalk.SpecFlow.Table(new string[] {
                        "To",
                        "Subject",
                        "Body"});
            table17.AddRow(new string[] {
                        "test1@freemail.com",
                        "[[my(-1).subject]] =",
                        "testing email from the cool specflow"});
#line 125
 testRunner.And("the debug inputs as", ((string)(null)), table17, "And ");
#line hidden
            TechTalk.SpecFlow.Table table18 = new TechTalk.SpecFlow.Table(new string[] {
                        ""});
            table18.AddRow(new string[] {
                        "[[result]] ="});
#line 128
 testRunner.And("the debug output as", ((string)(null)), table18, "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Send New Exchange email with a negative index recordset for Body")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "ExchangeEmail")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("Exchange")]
        public virtual void SendNewExchangeEmailWithANegativeIndexRecordsetForBody()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Send New Exchange email with a negative index recordset for Body", ((string[])(null)));
#line 132
this.ScenarioSetup(scenarioInfo);
#line 133
 testRunner.And("new to address is \"test1@freemail.com\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 134
 testRunner.And("new exchange body is \"[[my(-1).body]]\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 135
 testRunner.When("the new exchange email tool is executed", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 136
 testRunner.Then("the new exchange email result will be \"\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 137
 testRunner.And("the new exchange execution has \"AN\" error", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table19 = new TechTalk.SpecFlow.Table(new string[] {
                        "To",
                        "Subject",
                        "Body"});
            table19.AddRow(new string[] {
                        "test1@freemail.com",
                        "\"\"",
                        "[[my(-1).body]] ="});
#line 138
 testRunner.And("the debug inputs as", ((string)(null)), table19, "And ");
#line hidden
            TechTalk.SpecFlow.Table table20 = new TechTalk.SpecFlow.Table(new string[] {
                        ""});
            table20.AddRow(new string[] {
                        "[[result]] ="});
#line 141
 testRunner.And("the debug output as", ((string)(null)), table20, "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Send New Exchange email with new line in body")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "ExchangeEmail")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("Exchange")]
        public virtual void SendNewExchangeEmailWithNewLineInBody()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Send New Exchange email with new line in body", ((string[])(null)));
#line 145
this.ScenarioSetup(scenarioInfo);
#line 146
 testRunner.Given("new exchange to address is \"test1@freemail.com\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 147
 testRunner.And("the new exchange subject is \"Testing this cool framework\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 148
 testRunner.And("new exchange body is \"testing email with \\r\\n new line\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 149
 testRunner.When("the new exchange email tool is executed", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 150
 testRunner.Then("the new exchange email result will be \"Success\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 151
 testRunner.And("the new exchange execution has \"NO\" error", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table21 = new TechTalk.SpecFlow.Table(new string[] {
                        "To",
                        "Subject",
                        "Body"});
            table21.AddRow(new string[] {
                        "test1@freemail.com",
                        "Testing this cool framework",
                        "testing email with \\r\n new line"});
#line 152
 testRunner.And("the debug inputs as", ((string)(null)), table21, "And ");
#line hidden
            TechTalk.SpecFlow.Table table22 = new TechTalk.SpecFlow.Table(new string[] {
                        ""});
            table22.AddRow(new string[] {
                        "[[result]] = Success"});
#line 155
 testRunner.And("the debug output as", ((string)(null)), table22, "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Send New Exchange email with Html false")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "ExchangeEmail")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("Exchange")]
        public virtual void SendNewExchangeEmailWithHtmlFalse()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Send New Exchange email with Html false", ((string[])(null)));
#line 159
this.ScenarioSetup(scenarioInfo);
#line 160
 testRunner.Given("new exchange to address is \"\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 161
 testRunner.And("the new exchange subject is \"Just testing\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 162
 testRunner.And("new exchange body is \"testing email from the cool specflow\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 163
 testRunner.And("new exchange IsHtml is \"true\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 164
 testRunner.When("the new exchange email tool is executed \"\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 165
 testRunner.Then("the new exchange email result will be \"\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            TechTalk.SpecFlow.Table table23 = new TechTalk.SpecFlow.Table(new string[] {
                        "To",
                        "Subject",
                        "Body"});
            table23.AddRow(new string[] {
                        "\"\"",
                        "Just testing",
                        "testing email from the cool specflow"});
#line 166
 testRunner.And("the debug inputs as", ((string)(null)), table23, "And ");
#line hidden
            TechTalk.SpecFlow.Table table24 = new TechTalk.SpecFlow.Table(new string[] {
                        ""});
            table24.AddRow(new string[] {
                        "[[result]] ="});
#line 169
 testRunner.And("the debug output as", ((string)(null)), table24, "And ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
