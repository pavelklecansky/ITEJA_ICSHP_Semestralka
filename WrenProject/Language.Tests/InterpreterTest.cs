using System;
using Xunit;

namespace Language.Tests
{
    public class IntepreterTest
    {
        [Fact]
        public void Print01()
        {
            var output = new OutputWriter("");
            Console.SetOut(output);
            var source = @"System.print(15)";

            Wren.Run(source, output);

            Assert.Equal("15\n", output.output);
        }

        [Fact]
        public void Print02()
        {
            var output = new OutputWriter("");
            Console.SetOut(output);
            var source = @"System.print(""Test"")";

            Wren.Run(source, output);

            Assert.Equal("Test\n", output.output);
        }

        [Fact]
        public void Print03()
        {
            var output = new OutputWriter("");
            Console.SetOut(output);
            var source = @"System.print(15*2*4*10)";

            Wren.Run(source, output);

            Assert.Equal("1200\n", output.output);
        }

        [Fact]
        public void Print04()
        {
            var output = new OutputWriter("");
            Console.SetOut(output);
            var source = @"System.print(25/5/5)";

            Wren.Run(source, output);


            Assert.Equal("1\n", output.output);
        }

        [Fact]
        public void Print05()
        {
            var output = new OutputWriter("");
            Console.SetOut(output);
            var source = @"System.print(25%5)";

            Wren.Run(source, output);

            Assert.Equal("0\n", output.output);
        }

        [Fact]
        public void Print06()
        {
            var output = new OutputWriter("");
            Console.SetOut(output);
            var source = @"System.print(25+5-10)";

            Wren.Run(source, output);

            Assert.Equal("20\n", output.output);
        }


        [Fact]
        public void Print07_Unary()
        {
            var output = new OutputWriter("");
            Console.SetOut(output);
            var source = @"System.print(-25+1)";

            Wren.Run(source, output);

            Assert.Equal("-24\n", output.output);
        }

        [Fact]
        public void Print08_Unary()
        {
            var output = new OutputWriter("");
            Console.SetOut(output);
            var source = @"System.print(+25+1)";

            Wren.Run(source, output);

            Assert.Equal("26\n", output.output);
        }

        [Fact]
        public void Print09_Unary()
        {
            var output = new OutputWriter("");
            Console.SetOut(output);
            var source = @"System.print(*25+1)";

            Assert.Throws<ArgumentException>(() => Wren.Run(source, output));
        }

        [Fact]
        public void Print10_Variable()
        {
            var output = new OutputWriter("");
            Console.SetOut(output);
            var source = @"var test = 15
                           System.print(test)";
            Wren.Run(source, output);
            Assert.Equal("15\n", output.output);
        }

        [Fact]
        public void Print11_Variable()
        {
            var output = new OutputWriter("");
            Console.SetOut(output);
            var source = @"var test = ""Pes""
                           System.print(test)";
            Wren.Run(source, output);
            Assert.Equal("Pes\n", output.output);
        }

        [Fact]
        public void Print12_Variable()
        {
            var output = new OutputWriter("");
            Console.SetOut(output);
            var source = @"System.print(test)";

            Assert.Throws<ArgumentException>(() => Wren.Run(source, output));
        }


        [Fact]
        public void Print13_If()
        {
            var output = new OutputWriter("");
            Console.SetOut(output);
            var source = @"if(5>10){System.print(1)}else{System.print(0)}";

            Wren.Run(source, output);
            Assert.Equal("0\n", output.output);
        }
        
        [Fact]
        public void Print14_If()
        {
            var output = new OutputWriter("");
            Console.SetOut(output);
            var source = @"if(15>10){System.print(1)}else{System.print(0)}";

            Wren.Run(source, output);
            Assert.Equal("1\n", output.output);
        }
        
        [Fact]
        public void Print15_If()
        {
            var output = new OutputWriter("");
            Console.SetOut(output);
            var source = @"if(10>=10){System.print(1)}else{System.print(0)}";

            Wren.Run(source, output);
            Assert.Equal("1\n", output.output);
        }
        
         
        [Fact]
        public void Print16_If()
        {
            var output = new OutputWriter("");
            Console.SetOut(output);
            var source = @"if(10<=10){System.print(1)}else{System.print(0)}";

            Wren.Run(source, output);
            Assert.Equal("1\n", output.output);
        }
        
        [Fact]
        public void Print17_If()
        {
            var output = new OutputWriter("");
            Console.SetOut(output);
            var source = @"if(10==10){System.print(1)}else{System.print(0)}";

            Wren.Run(source, output);
            Assert.Equal("1\n", output.output);
        }
        
        [Fact]
        public void Print18_If()
        {
            var output = new OutputWriter("");
            Console.SetOut(output);
            var source = @"if(10!=10){System.print(1)}else{System.print(0)}";

            Wren.Run(source, output);
            Assert.Equal("0\n", output.output);
        }
        
        [Fact]
        public void Print19_If()
        {
            var output = new OutputWriter("");
            Console.SetOut(output);
            var source = @"if(10 < 10){System.print(1)}else{System.print(0)}";

            Wren.Run(source, output);
            Assert.Equal("0\n", output.output);
        }
        
        
        [Fact]
        public void Print19_While()
        {
            var output = new OutputWriter("");
            Console.SetOut(output);
            var source = @"var test = 5
                           while(test < 10){
                            System.print(test)
                            test = test + 1                            
                           }";

            Wren.Run(source, output);
            Assert.Equal("5\n6\n7\n8\n9\n", output.output);
        }
    }
}