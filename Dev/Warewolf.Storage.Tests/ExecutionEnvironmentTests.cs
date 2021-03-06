﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Dev2.Common.Common;
using Dev2.Common.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json.Linq;
using WarewolfParserInterop;
using Dev2.Data.Util;

namespace Warewolf.Storage.Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class ExecutionEnvironmentTest
    {
        ExecutionEnvironment _environment;

        const string OutOfBoundExpression = "[[rec(0).a]]";
        const string InvalidScalar = "[[rec(0).a]";
        const string PersonNameExpression = "[[@Person().Name]]";
        const string ChildNameExpression = "[[@Person.Child().Name]]";
        const string VariableA = "[[a]]";


        readonly CommonFunctions.WarewolfEvalResult _warewolfEvalNothingResult =
            CommonFunctions.WarewolfEvalResult.NewWarewolfAtomResult(DataStorage.WarewolfAtom.Nothing);

        [TestInitialize]
        public void MockSetup()
        {
            _environment = new ExecutionEnvironment();
            Assert.IsNotNull(_environment);
        }

        [TestMethod]
        [Owner("Sanele Mthembu")]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GivenInvalidIndex_ExecutionEnvironmentEval_ShouldThrowIndexOutOfRangeException()
        {
            Assert.IsNotNull(_environment);
            _environment.Eval(OutOfBoundExpression, 0, true);
        }

        [TestMethod]
        [Owner("Sanele Mthembu")]
        public void GivenRecSetName_ExecutionEnvironmentGetCount_ShouldReturn1()
        {
            Assert.IsNotNull(_environment);
            _environment.Assign("[[rec().a]]", "sanele", 0);
            var recordSet = _environment.GetCount("rec");
            Assert.AreEqual(1, recordSet);
        }

        [TestMethod]
        [Owner("Sanele Mthembu")]
        public void GivenRecSet_ExecutionEnvironmentEvalAssignFromNestedLast_Should()
        {
            Assert.IsNotNull(_environment);
            var evalMultiAssign = EvalMultiAssign();
            var items = PublicFunctions.EvalEnvExpression("[[rec(*).a]]", 0, false, evalMultiAssign);
            var warewolfAtomListresult = items as CommonFunctions.WarewolfEvalResult.WarewolfAtomListresult;
            _environment.EvalAssignFromNestedLast("[[rec(*).a]]", warewolfAtomListresult, 0);
        }

        [TestMethod]
        [Owner("Rory McGuire")]
        public void GivenRecSet_ExecutionEnvironmentEvalAssignFromNestedLast_TwoColumn_Should()
        {
            Assert.IsNotNull(_environment);
            var evalMultiAssign = EvalMultiAssignTwoColumn();
            var items = PublicFunctions.EvalEnvExpression("[[rec(*).a]]", 0, false, evalMultiAssign);
            var warewolfAtomListresult = items as CommonFunctions.WarewolfEvalResult.WarewolfAtomListresult;
            _environment.EvalAssignFromNestedLast("[[rec().a]]", warewolfAtomListresult, 0);
            items = PublicFunctions.EvalEnvExpression("[[rec(*).b]]", 0, false, evalMultiAssign);
            warewolfAtomListresult = items as CommonFunctions.WarewolfEvalResult.WarewolfAtomListresult;
            _environment.EvalAssignFromNestedLast("[[rec().b]]", warewolfAtomListresult, 0);
            items = PublicFunctions.EvalEnvExpression("[[rec(*).c]]", 0, false, evalMultiAssign);
            warewolfAtomListresult = items as CommonFunctions.WarewolfEvalResult.WarewolfAtomListresult;
            _environment.EvalAssignFromNestedLast("[[rec().c]]", warewolfAtomListresult, 0);
            evalMultiAssign = EvalMultiAssignTwoColumn();
            items = PublicFunctions.EvalEnvExpression("[[rec(*).a]]", 0, false, evalMultiAssign);
            warewolfAtomListresult = items as CommonFunctions.WarewolfEvalResult.WarewolfAtomListresult;
            _environment.EvalAssignFromNestedLast("[[rec().a]]", warewolfAtomListresult, 0);
            items = PublicFunctions.EvalEnvExpression("[[rec(*).b]]", 0, false, evalMultiAssign);
            warewolfAtomListresult = items as CommonFunctions.WarewolfEvalResult.WarewolfAtomListresult;
            _environment.EvalAssignFromNestedLast("[[rec().b]]", warewolfAtomListresult, 0);
            items = PublicFunctions.EvalEnvExpression("[[rec(*).c]]", 0, false, evalMultiAssign);
            warewolfAtomListresult = items as CommonFunctions.WarewolfEvalResult.WarewolfAtomListresult;
            _environment.EvalAssignFromNestedLast("[[rec().c]]", warewolfAtomListresult, 0);

            var list_a = _environment.EvalAsListOfStrings("[[rec(*).a]]", 0);
            var list_b = _environment.EvalAsListOfStrings("[[rec(*).b]]", 0);
            var list_c = _environment.EvalAsListOfStrings("[[rec(*).c]]", 0);
            Assert.AreEqual(list_a.Count, 4);
            Assert.AreEqual(list_b.Count, 4);
            Assert.AreEqual(list_c.Count, 4);

            Assert.IsTrue(list_a[0].Equals("a11"));
            Assert.IsTrue(list_a[1].Equals("ayy"));
            Assert.IsTrue(list_a[2].Equals("a11"));
            Assert.IsTrue(list_a[3].Equals("ayy"));

            Assert.IsTrue(list_b[0].Equals(""));
            Assert.IsTrue(list_b[1].Equals("b33"));
            Assert.IsTrue(list_b[2].Equals(""));
            Assert.IsTrue(list_b[3].Equals("b33"));

            Assert.IsTrue(list_c[0].Equals("c22"));
            Assert.IsTrue(list_c[1].Equals("czz"));
            Assert.IsTrue(list_c[2].Equals("c22"));
            Assert.IsTrue(list_c[3].Equals("czz"));
        }

        [TestMethod]
        [Owner("Sanele Mthembu")]
        public void GivenNonExistingRec_ExecutionEnvironmentEvalAssignFromNestedLast_ShouldAddStar()
        {
            Assert.IsNotNull(_environment);
            _environment.Assign("[[rec().a]]", "sanele", 0);
            var evalMultiAssign = EvalMultiAssign();
            var items = PublicFunctions.EvalEnvExpression("[[rec(*).a]]", 0, false, evalMultiAssign);
            var warewolfAtomListresult = items as CommonFunctions.WarewolfEvalResult.WarewolfAtomListresult;
            _environment.EvalAssignFromNestedLast("[[recs().a]]", warewolfAtomListresult, 0);
        }

        [TestMethod]
        [Owner("Rory McGuire")]
        public void ExecutionEnvironmentGetLengthOfJson_ShouldThrow()
        {
            Assert.IsNotNull(_environment);
            string msg = null;
            try
            {
                _environment.GetLength("@obj.people");
            } catch (Exception e)
            {
                msg = e.Message;
            }
            Assert.AreEqual("not a recordset", msg);
        }

        [TestMethod]
        [Owner("Rory McGuire")]
        public void ExecutionEnvironmentGetObjectLengthOfJson_ShouldThrow()
        {
            Assert.IsNotNull(_environment);
            string msg = null;
            try
            {
                _environment.GetObjectLength("@obj.people");
            } catch (Exception e)
            {
                msg = e.Message;
            }
            Assert.AreEqual("not a json array", msg);
        }

        [TestMethod]
        [Owner("Sanele Mthembu")]
        public void ExecutionEnvironmentAssignFromNestedStar_Should()
        {
            Assert.IsNotNull(_environment);
            _environment.Assign("[[rec().a]]", "sanele", 0);
            var evalMultiAssign = EvalMultiAssign();
            var items = PublicFunctions.EvalEnvExpression("[[rec(*).a]]", 0, false, evalMultiAssign);
            var warewolfAtomListresult = items as CommonFunctions.WarewolfEvalResult.WarewolfAtomListresult;
            _environment.EvalAssignFromNestedStar("[[rec().a]]", warewolfAtomListresult, 0);
        }

        [TestMethod]
        [Owner("Sanele Mthembu")]
        public void ExecutionEnvironmentSortRecordSet_ShouldSortRecordSets()
        {
            Assert.IsNotNull(_environment);
            _environment.Assign("[[rec().a]]", "sanele", 0);
            var evalMultiAssign = EvalMultiAssign();
            PublicFunctions.EvalEnvExpression("[[rec(*).a]]", 0, false, evalMultiAssign);
            _environment.SortRecordSet("[[rec().a]]", true, 0);
        }

        static DataStorage.WarewolfEnvironment EvalMultiAssign()
        {
            var assigns = new List<IAssignValue>
            {
                new AssignValue("[[rec(1).a]]", "27"),
                new AssignValue("[[rec(3).a]]", "33"),
                new AssignValue("[[rec(2).a]]", "25")
            };
            var envEmpty = WarewolfTestData.CreateTestEnvEmpty("");
            var evalMultiAssign = PublicFunctions.EvalMultiAssign(assigns, 0, envEmpty);
            return evalMultiAssign;
        }
        static DataStorage.WarewolfEnvironment EvalMultiAssignTwoColumn()
        {
            var assigns = new List<IAssignValue>
            {
                new AssignValue("[[rec().a]]", "a11"),
                new AssignValue("[[rec().b]]", ""),
                new AssignValue("[[rec().c]]", "c22"),
                new AssignValue("[[rec().a]]", "ayy"),
                new AssignValue("[[rec().b]]", "b33"),
                new AssignValue("[[rec().c]]", "czz")
            };
            var envEmpty = WarewolfTestData.CreateTestEnvEmpty("");
            var evalMultiAssign = PublicFunctions.EvalMultiAssign(assigns, 0, envEmpty);
            return evalMultiAssign;
        }

        [TestMethod]
        [Owner("Hagashen Naidu")]
        [TestCategory("ExecutionEnvironment_EvalAsList")]
        public void ExecutionEnvironment_EvalAsList_WhenRecSet_ShouldReturnListOfAllValues()
        {
            //------------Setup for test--------------------------
            _environment.Assign("[[rec(1).a]]", "27", 0);
            _environment.Assign("[[rec(1).b]]", "bob", 0);
            _environment.Assign("[[rec(2).a]]", "31", 0);
            _environment.Assign("[[rec(2).b]]", "mary", 0);
            //------------Execute Test---------------------------
            var list = _environment.EvalAsList("[[rec(*)]]", 0).ToList();
            //------------Assert Results-------------------------
            Assert.IsNotNull(list);
            Assert.AreEqual("27", list[0].ToString());
            Assert.AreEqual("31", list[1].ToString());
            Assert.AreEqual("bob", list[2].ToString());
            Assert.AreEqual("mary", list[3].ToString());
        }

        [TestMethod]
        [Owner("Nkosinathi Sangweni")]
        [TestCategory("ExecutionEnvironment_EvalAsList")]
        public void ExecutionEnvironment_EvalAsList_WhenRecSet_ShouldReturnListOfAllValues_PadLeft()
        {
            //------------Setup for test--------------------------
            _environment.Assign("[[rec(1).a]]", "27     ", 0);
            _environment.Assign("[[rec(1).b]]", "bob    ", 0);
            _environment.Assign("[[rec(2).a]]", "31 ", 0);
            _environment.Assign("[[rec(2).b]]", "mary", 0);
            //------------Execute Test---------------------------
            var list = _environment.EvalAsList("[[rec(*)]]", 0).ToList();
            //------------Assert Results-------------------------
            Assert.IsNotNull(list);
            Assert.AreEqual("27     ", list[0].ToString());
            Assert.AreEqual("31 ", list[1].ToString());
            Assert.AreEqual("bob    ", list[2].ToString());
            Assert.AreEqual("mary", list[3].ToString());
        }
        
        [TestMethod]
        [Owner("Hagashen Naidu")]
        [TestCategory("ExecutionEnvironment_EvalAsList")]
        public void ExecutionEnvironment_EvalAsListOfString_WhenRecSet_ShouldReturnListOfAllValues()
        {
            //------------Setup for test--------------------------
            _environment.Assign("[[rec(1).a]]", "27", 0);
            _environment.Assign("[[rec(1).b]]", "bob", 0);
            _environment.Assign("[[rec(2).a]]", "31", 0);
            _environment.Assign("[[rec(2).b]]", "mary", 0);
            //------------Execute Test---------------------------
            var list = _environment.EvalAsListOfStrings("[[rec(*)]]", 0).ToList();
            //------------Assert Results-------------------------
            Assert.IsNotNull(list);
            Assert.AreEqual("27", list[0]);
            Assert.AreEqual("31", list[1]);
            Assert.AreEqual("bob", list[2]);
            Assert.AreEqual("mary", list[3]);
        }


        [TestMethod]
        [Owner("Hagashen Naidu")]
        [TestCategory("ExecutionEnvironment_EvalAsList")]
        public void ExecutionEnvironment_EvalAsString_WhenRecSet_ShouldReturnListOfAllValues()
        {
            //------------Setup for test--------------------------
            _environment.Assign("[[rec(1).a]]", "27", 0);
            _environment.Assign("[[rec(1).b]]", "bob", 0);
            _environment.Assign("[[rec(2).a]]", "31", 0);
            _environment.Assign("[[rec(2).b]]", "mary", 0);
            //------------Execute Test---------------------------
            var stringVal = ExecutionEnvironment.WarewolfEvalResultToString(_environment.Eval("[[rec(*)]]", 0));
            //------------Assert Results-------------------------
            Assert.IsNotNull(stringVal);
            Assert.AreEqual("27,31,bob,mary", stringVal);
        }


        [TestMethod]
        [Owner("Sanele Mthembu")]
        public void GivenJsonObject_ExecutionEnvironmentEvalAsListOfStrings_ShouldReturnValue()
        {
            Assert.IsNotNull(_environment);
            _environment.Assign("[[Person().Name]]", "Sanele", 0);
            _environment.Assign("[[Person().Name]]", "Nathi", 0);
            var evalAsListOfStrings = _environment.EvalAsListOfStrings("[[Person(*).Name]]", 0);
            Assert.IsTrue(evalAsListOfStrings.Contains("Sanele"));
        }

        [TestMethod]
        [Owner("Sanele Mthembu")]
        public void GivenListResults_ExecutionEnvironmentEvalAsListOfStrings_ShouldReturnValuesAsList()
        {
            Assert.IsNotNull(_environment);
            _environment.AssignJson(new AssignValue(PersonNameExpression, "Sanele"), 0);
            var evalAsListOfStrings = _environment.EvalAsListOfStrings(PersonNameExpression, 0);
            Assert.IsTrue(evalAsListOfStrings.Contains("Sanele"));
        }

        [TestMethod]
        [Owner("Sanele Mthembu")]
        public void ExecutionEnvironmentAssignWithFrameAndList_Should()
        {
            Assert.IsNotNull(_environment);
            _environment.AssignWithFrameAndList(VariableA,
                new WarewolfAtomList<DataStorage.WarewolfAtom>(DataStorage.WarewolfAtom.NewDataString("Test Value")),
                false, 0);
        }

        [TestMethod]
        [Owner("Sanele Mthembu")]
        public void GivenRecSet_ExecutionEnvironmentHasRecordSet_ShouldReturnTrue()
        {
            var executionEnv = new ExecutionEnvironment();
            executionEnv.Assign("[[rec().a]]", "bob", 0);
            var hasRecordSet = executionEnv.HasRecordSet("[[rec()]]");
            Assert.IsTrue(hasRecordSet);
        }

        [TestMethod]
        [Owner("Sanele Mthembu")]
        public void GivenIncorrectString_ExecutionEnvironmentHasRecordSet_ShouldReturnFalse()
        {
            Assert.IsNotNull(_environment);
            var hasRecordSet = _environment.HasRecordSet("RandomString");
            Assert.IsFalse(hasRecordSet);
        }

        [TestMethod]
        [Owner("Sanele Mthembu")]
        public void GivenJson_ExecutionEnvironmentToStar_ShouldReturnAddStar()
        {

            Assert.IsNotNull(_environment);
            var star = _environment.ToStar(PersonNameExpression);
            Assert.IsNotNull(star);
            Assert.AreEqual("[[@Person(*).Name]]", star);
        }

        [TestMethod]
        [Owner("Sanele Mthembu")]
        public void GivenRecSet_ExecutionEnvironmentToStar_ShouldReturnAddStar()
        {
            var star = _environment.ToStar("[[rec().a]]");
            Assert.IsNotNull(star);
            Assert.AreEqual("[[rec(*).a]]", star);

            star = _environment.ToStar("[[rec()]]");
            Assert.IsNotNull(star);
            Assert.AreEqual("[[rec(*)]]", star);
        }

        [TestMethod]
        [Owner("Sanele Mthembu")]
        public void GivenVariable_ExecutionEnvironmentToStar_ShouldReturnTheSame()
        {
            Assert.IsNotNull(_environment);
            var star = _environment.ToStar(VariableA);
            Assert.IsNotNull(star);
            Assert.AreEqual(VariableA, star);
        }

        [TestMethod]
        [Owner("Sanele Mthembu")]
        public void ExecutionEnvironmentAssignFromNestedNumeric_Should()
        {
            Assert.IsNotNull(_environment);
            _environment.Assign("[[rec().a]]", "sanele", 0);
            var evalMultiAssign = EvalMultiAssign();
            var items = PublicFunctions.EvalEnvExpression("[[rec(*).a]]", 0, false, evalMultiAssign);
            var warewolfAtomListresult = items as CommonFunctions.WarewolfEvalResult.WarewolfAtomListresult;
            _environment.EvalAssignFromNestedNumeric("[[rec().a]]", warewolfAtomListresult, 0);
        }

        [TestMethod]
        [Owner("Sanele Mthembu")]
        public void ExecutionEnvironmentWarewolfEvalResultToString_Should()
        {
            var warewolfEvalResultToString = ExecutionEnvironment.WarewolfEvalResultToString(
                CommonFunctions.WarewolfEvalResult.NewWarewolfAtomListresult(
                    new WarewolfAtomList<DataStorage.WarewolfAtom>(DataStorage.WarewolfAtom.NewDataString("Test string"))));
            Assert.IsNotNull(warewolfEvalResultToString);

            warewolfEvalResultToString = ExecutionEnvironment.WarewolfEvalResultToString(
                CommonFunctions.WarewolfEvalResult.NewWarewolfAtomResult(
                    DataStorage.WarewolfAtom.NewDataString("Test string")));
            Assert.IsNotNull(warewolfEvalResultToString);
        }

        [TestMethod]
        [Owner("Sanele Mthembu")]
        public void GivenRecSet_ExecutionEnvironmentIsRecordSetName_ShouldReturnTrue()
        {
            Assert.IsNotNull(_environment);
            _environment.Assign("[[rec(1).a]]", "Test Value", 0);
            var isRecordSetName = ExecutionEnvironment.IsRecordSetName("[[rec()]]");
            Assert.IsTrue(isRecordSetName);
        }

        [TestMethod]
        [Owner("Sanele Mthembu")]
        public void GivenInvalidRecSet_ExecutionEnvironmentIsRecordSetName_ShouldReturnFalse()
        {
            Assert.IsNotNull(_environment);
            var isRecordSetName = ExecutionEnvironment.IsRecordSetName(InvalidScalar);
            Assert.IsFalse(isRecordSetName);
        }

        [TestMethod]
        [Owner("Sanele Mthembu")]
        public void GivenValidExpression_ExecutionEnvironmentIsValidVariableExpression_ShouldReturnTrue()
        {
            Assert.IsNotNull(_environment);
            var isValidVariableExpression = ExecutionEnvironment.IsValidVariableExpression(VariableA, out string message, 0);
            Assert.IsTrue(isValidVariableExpression);
        }

        [TestMethod]
        [Owner("Sanele Mthembu")]
        public void GivenInValidExpressionOrEmptyString_ExecutionEnvironmentIsValidVariableExpression_ShouldReturnFalse()
        {
            Assert.IsNotNull(_environment);
            //Given Invalid Scalar
            var isValidVariableExpression = ExecutionEnvironment.IsValidVariableExpression(InvalidScalar, out string message, 0);
            Assert.IsFalse(isValidVariableExpression);
            //Given Empty Strign
            isValidVariableExpression = ExecutionEnvironment.IsValidVariableExpression(string.Empty, out message, 0);
            Assert.IsFalse(isValidVariableExpression);
        }

        [TestMethod]
        [Owner("Sanele Mthembu")]
        public void ExecutionEnvironmentGetLength_Should()
        {
            Assert.IsNotNull(_environment);
            _environment.Assign("[[rec().a]]", "sanele", 0);
            var recordSet = _environment.GetLength("rec");
            Assert.AreEqual(1, recordSet);
        }

        [TestMethod]
        [Owner("Nkosinathi Sangweni")]
        public void ExecutionEnvironmentGetObjectLength_Should()
        {
            Assert.IsNotNull(_environment);
            _environment.AssignJson(new AssignValue("[[@Obj()]]", "{\"PolicyNo\":\"A0003\",\"DateId\":32,\"SomeVal\":\"Bob\"}"), 0);
            var recordSet = _environment.GetObjectLength("Obj");
            Assert.AreEqual(1, recordSet);
        }

        [TestMethod]
        [Owner("Sanele Mthembu")]
        public void ExecutionEnvironmentEvalToExpression_Should()
        {
            Assert.IsNotNull(_environment);
            _environment.Assign(VariableA, "SomeValue", 0);
            var evalToExpression = _environment.EvalToExpression(VariableA, 0);
            Assert.AreEqual("[[a]]", evalToExpression);
        }

        [TestMethod]
        [Owner("Hagashen Naidu")]
        public void ExecutionEnvironmentEvalComplexCalcExpression_ShouldNotReplaceSpaces()
        {
            Assert.IsNotNull(_environment);
            _environment.Assign("[[FirstNames]]", "Bob", 0);
            var calcExpr = "!~calculation~!FIND(\" \",[[FirstNames]],1)!~~calculation~!";
            var evalResult = _environment.Eval(calcExpr, 0);
            Assert.AreEqual("!~calculation~!FIND(\" \",\"Bob\",1)!~~calculation~!", ExecutionEnvironment.WarewolfEvalResultToString(evalResult));
        }

        [TestMethod]
        [Owner("Sanele Mthembu")]
        public void GivenRecSet_ExecutionEnvironmentGetPositionColumnExpression_ShouldReturn()
        {
            Assert.IsNotNull(_environment);
            var positionColumnExpression = ExecutionEnvironment.GetPositionColumnExpression("[[rec()]]");
            Assert.IsNotNull(positionColumnExpression);
            Assert.IsTrue(positionColumnExpression.Contains("rec(*)"));
        }

        [TestMethod]
        [Owner("Sanele Mthembu")]
        public void GivenVariable_ExecutionEnvironmentGetPositionColumnExpression_ShouldReturnSameVariable()
        {
            Assert.IsNotNull(_environment);
            var positionColumnExpression = ExecutionEnvironment.GetPositionColumnExpression("[[rec]]");
            Assert.IsNotNull(positionColumnExpression);
            Assert.AreEqual("[[rec]]", positionColumnExpression);
        }

        [TestMethod]
        [Owner("Sanele Mthembu")]
        public void ExecutionEnvironmentConvertToIndex_Should()
        {
            Assert.IsNotNull(_environment);
            var convertToIndex = ExecutionEnvironment.ConvertToIndex(VariableA, 0);
            Assert.IsNotNull(convertToIndex);
            convertToIndex = ExecutionEnvironment.ConvertToIndex("[[rec(1).a]]", 0);
            Assert.IsNotNull(convertToIndex);

            convertToIndex = ExecutionEnvironment.ConvertToIndex("[[rec(*).a]]", 0);
            Assert.IsNotNull(convertToIndex);
        }

        [TestMethod]
        [Owner("Sanele Mthembu")]
        public void GivenVariable_ExecutionEnvironmentIsScalar_ShouldBeTrue()
        {

            Assert.IsNotNull(_environment);
            var isScalar = ExecutionEnvironment.IsScalar(VariableA);
            Assert.IsTrue(isScalar);
        }

        [TestMethod]
        [Owner("Sanele Mthembu")]
        public void GivenInvalidScarOrSomeString_ExecutionEnvironmentIsScalar_ShouldBeFalse()
        {

            Assert.IsNotNull(_environment);
            var isScalar = ExecutionEnvironment.IsScalar("SomeString");
            Assert.IsFalse(isScalar);
            isScalar = ExecutionEnvironment.IsScalar("[[a]");
            Assert.IsFalse(isScalar);
        }


        [TestMethod]
        [Owner("Sanele Mthembu")]
        public void ExecutionEnvironmentEvalAsList_Should()
        {

            Assert.IsNotNull(_environment);
            var evalAsList = _environment.EvalAsList(PersonNameExpression, 0);
            Assert.IsNotNull(evalAsList);
            evalAsList = _environment.EvalAsList(string.Empty, 0);
            Assert.IsNotNull(evalAsList);
        }

        [TestMethod]
        [Owner("Sanele Mthembu")]
        public void ExecutionEnvironmentApplyUpdate_Should()
        {
            Assert.IsNotNull(_environment);
            _environment.Assign(VariableA, "SomeValue", 0);
            var clause =
                new Func<DataStorage.WarewolfAtom, DataStorage.WarewolfAtom>(atom => atom);
            _environment.ApplyUpdate(VariableA, clause, 0);
        }

        [TestMethod]
        [Owner("Sanele Mthembu")]
        public void GivenIsNothingEval_ExecutionEnvironmentEvalWhere_ShouldReturnNothing()
        {
            Assert.IsNotNull(_environment);
            _environment.Assign(VariableA, "SomeValue", 0);
            var clause = new Func<DataStorage.WarewolfAtom, bool>(atom => atom.IsNothing);
            var evalWhere = _environment.EvalWhere("[[rec()]]", clause, 0);
            Assert.IsNotNull(evalWhere);
        }

        [TestMethod]
        [Owner("Sanele Mthembu")]
        public void GivenJSonExpression_ExecutionEnvironmentGetIndexes_ShouldReturn1Index()
        {
            Assert.IsNotNull(_environment);
            _environment.AssignJson(new AssignValue(PersonNameExpression, "Sanele"), 0);
            var indexes = _environment.GetIndexes(PersonNameExpression);
            Assert.AreEqual(1, indexes.Count);
            Assert.IsTrue(indexes.Contains(PersonNameExpression));
        }

        [TestMethod]
        [Owner("Sanele Mthembu")]
        public void GivenRecSet_ExecutionEnvironmentGetIndexes_ShouldReturn1Index()
        {
            Assert.IsNotNull(_environment);
            const string recA = "[[rec(*).a]]";
            _environment.Assign(recA, "Something", 0);
            var indexes = _environment.GetIndexes(recA);
            Assert.AreEqual(1, indexes.Count);
        }

        [TestMethod]
        [Owner("Sanele Mthembu")]
        public void GivenContainer_ExecutionEnvironmentBuildIndexMap_ShouldBuildMapForChildObj()
        {
            Assert.IsNotNull(_environment);
            _environment.AssignJson(new AssignValue(ChildNameExpression, "Sanele"), 0);
            var privateObj = new PrivateObject(_environment);
            var var = EvaluationFunctions.parseLanguageExpressionWithoutUpdate(ChildNameExpression);
            var jsonIdentifierExpression = var as LanguageAST.LanguageExpression.JsonIdentifierExpression;
            var obj = new JArray(ChildNameExpression);
            if (jsonIdentifierExpression == null)
            {
                return;
            }

            var mapItems = new List<string>();
            object[] args = { jsonIdentifierExpression.Item, "", mapItems, obj };
            privateObj.Invoke("BuildIndexMap", args);
        }

        [TestMethod]
        [Owner("Sanele Mthembu")]
        public void ExecutionEnvironmentEvalDelete_Should_Clear_RecordSet()
        {
            Assert.IsNotNull(_environment);
            _environment.Assign("[[rec().a]]", "Some Value", 0);
            _environment.EvalDelete("[[rec()]]", 0);
            var result = _environment.Eval("[[rec().a]]", 0);
            Assert.IsTrue(WarewolfDataEvaluationCommon.isNothing(result));
        }

        [TestMethod]
        [Owner("Sanele Mthembu")]
        public void GivenEmptyStringAndName_ExecutionEnvironmentAssignWithFrame_ShouldReturn()
        {
            Assert.IsNotNull(_environment);
            _environment.AssignWithFrame(new AssignValue(PersonNameExpression, "Value"), 0);
            try
            {
                _environment.AssignWithFrame(new AssignValue(string.Empty, "Value"), 0);
            }
            catch (Exception e)
            {
                Assert.AreEqual("invalid variable assigned to ", e.Message);
            }
        }

        [TestMethod]
        [Owner("Sanele Mthembu")]
        public void GivenEmptyStringAndName_ExecutionEnvironmentIsValidRecordSetIndex_ShouldReturn()
        {
            Assert.IsNotNull(_environment);
            Assert.IsTrue(ExecutionEnvironment.IsValidRecordSetIndex("[[rec().a]]"));
        }

        [TestMethod]
        [Owner("Sanele Mthembu")]
        public void ExecutionEnvironmentAssignDataShape_ShouldReturn()
        {
            Assert.IsNotNull(_environment);
            _environment.AssignDataShape("[[SomeString]]");
        }

        [TestMethod]
        [Owner("Sanele Mthembu")]
        [ExpectedException(typeof(Exception))]
        public void GivenInvalidScalar_ExecutionEnvironmentAssignWithFrame_ShouldThrowException()
        {
            Assert.IsNotNull(_environment);
            _environment.AssignWithFrame(new AssignValue(InvalidScalar, "Value"), 0);
        }

        [TestMethod]
        [Owner("Sanele Mthembu")]
        public void ExecutionEnvironmentEvalForDataMerge_Should()
        {
            Assert.IsNotNull(_environment);
            _environment.Assign(VariableA, "Sanele", 0);
            _environment.EvalForDataMerge(VariableA, 0);
        }

        [ExpectedException(typeof(NullValueInVariableException))]
        [TestMethod]
        [Owner("Sanele Mthembu")]
        public void GivenUnAssignedVar_ExecutionEnvironmentEvalStrict_ShouldThrowNullValueInVariableException()
        {
            Assert.IsNotNull(_environment);
            _environment.EvalStrict(PersonNameExpression, 0);
        }

        [TestMethod]
        [Owner("Sanele Mthembu")]
        public void ExecutionEnvironmentEvalStrict_Should()
        {
            Assert.IsNotNull(_environment);
            _environment.Assign(PersonNameExpression, "Sanele", 0);
            _environment.EvalStrict(PersonNameExpression, 0);
        }

        [TestMethod]
        [Owner("Sanele Mthembu")]
        public void GivenEmptyString_ExecutionEnvironmenAssign_ShouldReturn()
        {
            Assert.IsNotNull(_environment);
            _environment.Assign(string.Empty, string.Empty, 0);
        }

        [TestMethod]
        [Owner("Sanele Mthembu")]
        public void ExecutionEnvironmentAssignUnique_Should()
        {
            var recs = new List<string>
            {
                "[[Person().Name]]",
                "[[Person(1).Name]]",
                "[[Person(2).Name]]"
            };
            var values = new List<string> { "[[Person().Name]]" };

            Assert.IsNotNull(_environment);
            _environment.Assign("[[Person().Name]]", "sanele", 0);
            var resList = new List<string>();
            _environment.AssignUnique(recs, values, resList, 0);
            Assert.IsNotNull(resList);
        }

        [TestMethod]
        [Owner("Sanele Mthembu")]
        [ExpectedException(typeof(Exception))]
        public void GivenInvalidExpression_ExecutionEnvironmentEval_ShouldThrowException()
        {
            Assert.IsNotNull(_environment);
            const string expression = "[[rec(0).a]";
            _environment.Eval(expression, 0, true);
        }

        [TestMethod]
        [Owner("Sanele Mthembu")]
        public void GivenInvalidExpressionAndthrowsifnotexistsIsFalse_ExecutionEnvironmentEval_ShouldReturnNothing()
        {
            Assert.IsNotNull(_environment);
            const string expression = "[[rec(0).a]";
            var warewolfEvalResult = _environment.Eval(expression, 0);
            Assert.AreEqual(_warewolfEvalNothingResult, warewolfEvalResult);
        }

        [TestMethod]
        [Owner("Sanele Mthembu")]
        public void GivenEmptyString_ExecutionEnvironmentEvalJContainer_ShouldReturn()
        {

            Assert.IsNotNull(_environment);
            var evalJContainer = _environment.EvalJContainer(string.Empty);
            Assert.IsNull(evalJContainer);

            const string something = "new {string valu3};";
            evalJContainer = _environment.EvalJContainer(something);
            Assert.IsNull(evalJContainer);

            _environment.AssignJson(new AssignValue(PersonNameExpression, "Sanele"), 0);
            evalJContainer = _environment.EvalJContainer(PersonNameExpression);
            Assert.IsNotNull(evalJContainer);
        }

        [TestMethod]
        [Owner("Sanele Mthembu")]
        public void GivenEmptyString_ExecutionEnvironmentEvalForJason_ShouldReturnNothing()
        {
            Assert.IsNotNull(_environment);
            var warewolfEvalResult = _environment.EvalForJson(string.Empty);
            Assert.AreEqual(_warewolfEvalNothingResult, warewolfEvalResult);
        }

        [TestMethod]
        [Owner("Sanele Mthembu")]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GivenInvalidScalar_ExecutionEnvironmentEvalForJason_ShouldReturnNothing()
        {
            Assert.IsNotNull(_environment);
            var warewolfEvalResult = _environment.EvalForJson(OutOfBoundExpression);
            Assert.AreEqual(_warewolfEvalNothingResult, warewolfEvalResult);
        }

        [TestMethod]
        [Owner("Sanele Mthembu")]
        public void GivenInvalidScalar_ExecutionEnvironmentEvalForJason_ShouldException()
        {

            Assert.IsNotNull(_environment);
            var warewolfEvalResult = _environment.EvalForJson(InvalidScalar);
            Assert.AreEqual(_warewolfEvalNothingResult, warewolfEvalResult);
            warewolfEvalResult = _environment.EvalForJson("[[rec().a]]");
            Assert.IsNotNull(warewolfEvalResult);
        }

        [TestMethod]
        [Owner("Sanele Mthembu")]
        public void GivenEmptyString_ExecutionEnvironmentAssignJson_ShouldReturn()
        {

            Assert.IsNotNull(_environment);
            var values = new AssignValue(string.Empty, "John");
            _environment.AssignJson(values, 0);
        }

        [TestMethod]
        [Owner("Sanele Mthembu")]
        public void GivenObjectExecutionEnvironmentAssignJson_ShouldAddObject()
        {

            Assert.IsNotNull(_environment);
            var values = new List<IAssignValue> { new AssignValue("[[@Person.Name]]", "John") };
            _environment.AssignJson(values, 0);
        }

        [TestMethod]
        [Owner("Sanele Mthembu")]
        [ExpectedException(typeof(Exception))]
        public void GivenInvalidObject_ExecutionEnvironmentAssignJson_ShouldThrowParseError()
        {

            Assert.IsNotNull(_environment);
            var values = new AssignValue("[[@Person.Name]", "John");
            _environment.AssignJson(values, 0);
            Assert.AreEqual(1, _environment.Errors.Count);
        }
        [TestMethod]
        [Owner("Sanele Mthembu")]
        public void ExecutionEnvironment_ShouldHave_Ctor()
        {
            Assert.IsNotNull(_environment);
            var privateObj = new PrivateObject(_environment);
            var field = privateObj.GetField("_env");
            Assert.IsNotNull(field);
        }

        [TestMethod]
        [Owner("Sanele Mthembu")]
        public void ExecutionEnvironmentCtor_ShouldErrorsCountAs0()
        {
            Assert.IsNotNull(_environment);
            Assert.AreEqual(0, _environment.AllErrors.Count);
            Assert.AreEqual(0, _environment.Errors.Count);
        }

        [TestMethod]
        [Owner("Sanele Mthembu")]
        public void ExecutionEnvironmentWarewolfAtomToStringErrorIfNull_ShouldReturnStringEmpty()
        {
            const string expected = "SomeString";
            var givenSomeString = DataStorage.WarewolfAtom.NewDataString(expected);
            var result = ExecutionEnvironment.WarewolfAtomToStringErrorIfNull(givenSomeString);
            Assert.AreEqual(expected, result);

            result = ExecutionEnvironment.WarewolfAtomToStringErrorIfNull(null);
            Assert.IsTrue(string.IsNullOrEmpty(result));
        }

        [TestMethod]
        [Owner("Sanele Mthembu")]
        public void ExecutionEnvironmentAddToJsonObjects_ShouldAddJsonObject()
        {
            Assert.IsNotNull(_environment);
            _environment.AddToJsonObjects(PersonNameExpression, null);
            var privateObj = new PrivateObject(_environment);
            var field = privateObj.GetFieldOrProperty("_env") as DataStorage.WarewolfEnvironment;
            Assert.IsTrue(field != null && field.JsonObjects.Count > 0);
        }

        [ExpectedException(typeof(NullValueInVariableException))]
        [TestMethod]
        [Owner("Sanele Mthembu")]
        public void ExecutionEnvironmentWarewolfAtomToStringErrorIfNull_ShouldThrowAnError()
        {
            var atom = DataStorage.WarewolfAtom.Nothing;
            ExecutionEnvironment.WarewolfAtomToStringErrorIfNull(atom);
        }

        [TestMethod]
        public void ExecutionEnvironmentWarewolfAtomToStringNullAsNothing_ShouldReturnNull()
        {
            var givenNoting = DataStorage.WarewolfAtom.Nothing;
            var result = ExecutionEnvironment.WarewolfAtomToStringNullAsNothing(givenNoting);
            Assert.IsNull(result);

            var givenSomeString = DataStorage.WarewolfAtom.NewDataString("SomeString");
            result = ExecutionEnvironment.WarewolfAtomToStringNullAsNothing(givenSomeString);
            Assert.AreEqual(givenSomeString, result);

            result = ExecutionEnvironment.WarewolfAtomToStringNullAsNothing(null);
            Assert.IsNull(result);
        }

        [TestMethod]
        [Owner("Sanele Mthembu")]
        public void GivenNullForWarewolfAtom_ExecutionEnvironmentWarewolfAtomToString_ShouldReturnNull()
        {
            var result = ExecutionEnvironment.WarewolfAtomToString(null);
            Assert.IsTrue(string.IsNullOrEmpty(result));
            const string somestring = "SomeString";
            var atom = DataStorage.WarewolfAtom.NewDataString(somestring);
            result = ExecutionEnvironment.WarewolfAtomToString(atom);
            Assert.AreEqual(somestring, result);
        }

        [TestMethod]
        [Owner("Sanele Mthembu")]
        public void ExecutionEnvironmentAddError_ShouldIncreaseErrorCount()
        {
            Assert.IsNotNull(_environment);
            var countBefore = _environment.Errors.Count;
            Assert.AreEqual(0, _environment.Errors.Count);
            _environment.AddError(It.IsAny<string>());
            Assert.AreEqual(countBefore + 1, _environment.Errors.Count);
        }

        [TestMethod]
        [Owner("Sanele Mthembu")]
        public void GivenErrorsExecutionEnvironmentHasError_ShouldBeTrue()
        {
            var envWithErrors = CreateEnvironmentWithErrors();
            Assert.IsNotNull(envWithErrors);
            Assert.IsTrue(envWithErrors.Errors.Count > 0);
            Assert.IsTrue(envWithErrors.HasErrors());
        }

        [TestMethod]
        [Owner("Sanele Mthembu")]
        public void GivenErrorsAndAllErrorsHaveCount_ExecutionEnvironmentFetchError_ShouldJoinAllErrors()
        {
            var envWithErrors = CreateEnvironmentWithErrors();
            Assert.IsNotNull(envWithErrors);
            envWithErrors.AllErrors.Add("AnotherError");
            Assert.IsTrue(envWithErrors.Errors.Count > 0);
            Assert.IsTrue(envWithErrors.HasErrors());
            var expected = $"AnotherError{Environment.NewLine}SomeError";
            Assert.AreEqual(expected, envWithErrors.FetchErrors());
            expected = "{\"Environment\":{\"scalars\":{},\"record_sets\":{},\"json_objects\":{}},\"Errors\":[\"SomeError\"],\"AllErrors\":[\"AnotherError\"]}";
            Assert.AreEqual(expected, envWithErrors.ToJson());
        }

        [TestMethod]
        [Owner("Hagashen Naidu")]
        public void GivenJsonSerializedEnv_FromJson_ShouldSetValidEnvironment()
        {
            var serializedEnv= "{\"Environment\":{\"scalars\":{\"Name\":\"Bob\"},\"record_sets\":{\"R\":{\"FName\":[\"Bob\"],\"WarewolfPositionColumn\":[1]},\"Rec\":{\"Name\":[\"Bob\",,\"Bob\"],\"SurName\":[,\"Bob\",],\"WarewolfPositionColumn\":[1,3,4]},\"RecSet\":{\"Field\":[\"Bob\",\"Jane\"],\"WarewolfPositionColumn\":[1,2]}},\"json_objects\":{\"Person\":{\"Name\":\"B\"}}},\"Errors\":[],\"AllErrors\":[]}";
            _environment.FromJson(serializedEnv);
            var rec1Field1 = ExecutionEnvironment.WarewolfEvalResultToString(_environment.Eval("[[R(*).FName]]", 0));
            var rec2Field1 = ExecutionEnvironment.WarewolfEvalResultToString(_environment.Eval("[[Rec(*).Name]]", 0));
            var rec2Field2 = ExecutionEnvironment.WarewolfEvalResultToString(_environment.Eval("[[Rec(*).SurName]]", 0));
            var rec3Field1 = ExecutionEnvironment.WarewolfEvalResultToString(_environment.Eval("[[RecSet(*).Field]]", 0));
            var scalar = ExecutionEnvironment.WarewolfEvalResultToString(_environment.Eval("[[Name]]", 0));
            var jsonVal = ExecutionEnvironment.WarewolfEvalResultToString(_environment.Eval("[[@Person]]", 0));
            Assert.AreEqual("Bob", scalar);
            Assert.AreEqual("Bob", rec1Field1);
            Assert.AreEqual("Bob,,Bob", rec2Field1);
            Assert.AreEqual(",Bob,", rec2Field2);
            Assert.AreEqual("Bob,Jane", rec3Field1);
            Assert.AreEqual("{"+Environment.NewLine+"  \"Name\": \"B\""+Environment.NewLine+"}", jsonVal);
        }

        [TestMethod]
        [Owner("Hagashen Naidu")]
        public void GivenJsonSerializedEnv_FromJson_Invalid_ShouldNotUpdateEnvironment()
        {
            var serializedEnv = "some string";
            _environment.FromJson(serializedEnv);
            var hasRecSet = _environment.HasRecordSet("R");
            Assert.IsFalse(hasRecSet);
        }

        [TestMethod]
        [Owner("Hagashen Naidu")]
        public void GivenJsonSerializedEnv_FromJson_EmptyString_ShouldNotUpdateEnvironment()
        {
            var serializedEnv = "";
            _environment.FromJson(serializedEnv);
            var hasRecSet = _environment.HasRecordSet("R");
            Assert.IsFalse(hasRecSet);
        }

        [TestMethod]
        [Owner("Hagashen Naidu")]
        public void GivenJsonSerializedEnv_FromJson_NullString_ShouldNotUpdateEnvironment()
        {
            string serializedEnv = null;
            _environment.FromJson(serializedEnv);
            var hasRecSet = _environment.HasRecordSet("R");
            Assert.IsFalse(hasRecSet);
        }

        #region Private Methods

        ExecutionEnvironment CreateEnvironmentWithErrors()
        {
            _environment.Errors.Add("SomeError");
            return _environment;
        }

        #endregion        
    }
}