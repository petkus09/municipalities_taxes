using Municipalities.Cmd.Models;
using Municipalities.Cmd.Service;
using Municipalities.Logging.Contracts;
using NSubstitute;
using NUnit.Framework;
using System;

namespace Tests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void Execute_AllInvalidArguments_ExpectExceptions()
        {
            var command = new AddCommand(Substitute.For<IProgramLogger>());

            Assert.Throws<MunicipalityEmptyException>(() => command.Execute(
                new Municipalities.Cmd.Options.Add() { Municipality = " " }));
            Assert.Throws<TexRateBelow0Exception>(() => command.Execute(
                new Municipalities.Cmd.Options.Add() { Municipality = "something", TaxRate = -5 }));
            Assert.Throws<TaxTypeEmptyException>(() => command.Execute(
                new Municipalities.Cmd.Options.Add() { Municipality = "something", TaxRate = 0, TaxType = " " }));
            Assert.Throws<TaxTypeNotFoundException>(() => command.Execute(
                new Municipalities.Cmd.Options.Add() { Municipality = "something", TaxRate = 0, TaxType = "not supported" }));
            Assert.Throws<TaxRateStartDateEmptyException>(() => command.Execute(
                new Municipalities.Cmd.Options.Add() { Municipality = "something", TaxRate = 0, TaxType = "YEARLY", StartDate = " " }));
            Assert.Throws<TaxRateStartDateInvalidFormatException>(() => command.Execute(
                new Municipalities.Cmd.Options.Add() { Municipality = "something", TaxRate = 0, TaxType = "YEARLY", StartDate = "not a date" }));
            Assert.Throws<TaxRateStartDateInvalidFormatException>(() => command.Execute(
                new Municipalities.Cmd.Options.Add() { Municipality = "something", TaxRate = 0, TaxType = "YEARLY", StartDate = "aaaa-bb-cc" }));
        }

        [Test]
        public void Execute_ValidArguments_ExpectValidModel()
        {
            var command = new AddCommand(Substitute.For<IProgramLogger>());
            NewRecord res = null;
            command.NewRecordRequested += (NewRecord rec) => res = rec;

            Assert.IsTrue(command.Execute(
                new Municipalities.Cmd.Options.Add() { Municipality = "something", TaxRate = 100, TaxType = "DAILY", StartDate = "2019-05-06" }));
            Assert.AreEqual("something", res.Municipality);
            Assert.AreEqual(100, res.TaxRate);
            Assert.AreEqual(TaxScheduleType.Daily, res.TaxType);
            Assert.AreEqual(Convert.ToDateTime("2019-05-06"), res.StartDate);
        }
    }
}