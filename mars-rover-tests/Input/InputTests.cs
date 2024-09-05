using FluentAssertions;
using mars_rover;
using mars_rover.Entities;
using mars_rover.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace mars_rover_tests.Input
{
    public class InputTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test, Description("Parse valid Plateau Size")]
        public void TestParseValidPlateauSize()
        {
            var input = "5 5";

            var result = InputParser.ParsePlateauSize(input);

            result.Should().NotBeNull();
            result.Outcome.Should().Be(ParseOutcome.SUCCESS);
            result.ParsedValue.Should().BeEquivalentTo(new PlateauSize(5,5));
        }

        [Test, Description("Plateau Size fails to parse empty string")]
        public void TestParseEmptyStringPlateauSize()
        {
            var input = "";

            var result = InputParser.ParsePlateauSize(input);

            result.Should().NotBeNull();
            result.Outcome.Should().Be(ParseOutcome.INVALID_INPUT);
            result.ParsedValue.Should().BeNull();
        }

        [Test, Description("Plateau Size fails to parse null")]
        public void TestParseNullPlateauSize()
        {
            string input = null!;

            var result = InputParser.ParsePlateauSize(input!);

            result.Should().NotBeNull();
            result.Outcome.Should().Be(ParseOutcome.INVALID_INPUT);
            result.ParsedValue.Should().BeNull();
        }

        [Test, Description("Plateau Size fails to parse invalid string")]
        public void TestParseInvalidStringPlateauSize()
        {
            // TODO refactor to multiple test cases!
            var input = "aihreioha";

            var result = InputParser.ParsePlateauSize(input);

            result.Should().NotBeNull();
            result.Outcome.Should().Be(ParseOutcome.INVALID_INPUT);
            result.ParsedValue.Should().BeNull();

            input = "124tr534jioaj 2";
            result = InputParser.ParsePlateauSize(input);

            result.Should().NotBeNull();
            result.Outcome.Should().Be(ParseOutcome.INVALID_INPUT);
            result.ParsedValue.Should().BeNull();
        }

        [Test, Description("Parse valid Position")]
        public void TestParseValidPosition()
        {
            var input = "1 2 N";

            var result = InputParser.ParseLandingPosition(input);

            result.Should().NotBeNull();
            result.Outcome.Should().Be(ParseOutcome.SUCCESS);
            result.ParsedValue.Should().BeEquivalentTo(new Position(1, 2, CompassDirection.N));
        }

        [Test, Description("Position fails to parse empty string")]
        public void TestParseEmptyStringPosition()
        {
            var input = "";

            var result = InputParser.ParseLandingPosition(input);

            result.Should().NotBeNull();
            result.Outcome.Should().Be(ParseOutcome.INVALID_INPUT);
            result.ParsedValue.Should().BeNull();
        }

        [Test, Description("Position fails to parse null string")]
        public void TestParseNullPosition()
        {
            string input = null!;

            var result = InputParser.ParseLandingPosition(input);

            result.Should().NotBeNull();
            result.Outcome.Should().Be(ParseOutcome.INVALID_INPUT);
            result.ParsedValue.Should().BeNull();
        }

        [Test, Description("Position fails to parse invalid string")]
        public void TestParseInvalidStringPosition()
        {
            // TODO refactor to multiple test cases!
            var input = "aihreioha";

            var result = InputParser.ParseLandingPosition(input);

            result.Should().NotBeNull();
            result.Outcome.Should().Be(ParseOutcome.INVALID_INPUT);
            result.ParsedValue.Should().BeNull();

            input = "124tr534jioaj 2";
            result = InputParser.ParseLandingPosition(input);

            result.Should().NotBeNull();
            result.Outcome.Should().Be(ParseOutcome.INVALID_INPUT);
            result.ParsedValue.Should().BeNull();
        }



        [Test, Description("Parse valid instructions")]
        public void TestParseValidInstructions()
        {
            var input = "LRMLRM";

            var result = InputParser.ParseInstructions(input);

            result.Should().NotBeNull();
            result.Outcome.Should().Be(ParseOutcome.SUCCESS);
            result.ParsedValue.Should().BeEquivalentTo(new List<Instruction> {  
                Instruction.L,
                Instruction.R,
                Instruction.M,
                Instruction.L,
                Instruction.R,
                Instruction.M,
            });
        }

        [Test, Description("Instructions fails to parse empty string")]
        public void TestParseEmptyStringInstructions()
        {
            var input = "";

            var result = InputParser.ParseInstructions(input);

            result.Should().NotBeNull();
            result.Outcome.Should().Be(ParseOutcome.INVALID_INPUT);
            result.ParsedValue.Should().BeNull();
        }

        [Test, Description("Instructions fails to parse null string")]
        public void TestParseNullInstructions()
        {
            string input = null!;

            var result = InputParser.ParseInstructions(input);

            result.Should().NotBeNull();
            result.Outcome.Should().Be(ParseOutcome.INVALID_INPUT);
            result.ParsedValue.Should().BeNull();
        }

        [Test, Description("Instructions fails to parse invalid string")]
        public void TestParseInvalidStringInstructions()
        {
            // TODO refactor to multiple test cases!
            var input = "aihreioha";

            var result = InputParser.ParseInstructions(input);

            result.Should().NotBeNull();
            result.Outcome.Should().Be(ParseOutcome.INVALID_INPUT);
            result.ParsedValue.Should().BeNull();

            input = "124tr534jioaj 2";
            result = InputParser.ParseInstructions(input);

            result.Should().NotBeNull();
            result.Outcome.Should().Be(ParseOutcome.INVALID_INPUT);
            result.ParsedValue.Should().BeNull();
        }
    }
}
