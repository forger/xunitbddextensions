// Copyright 2009 Bj�rn Rochel - http://www.bjro.de/ 
//  
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//  
//      http://www.apache.org/licenses/LICENSE-2.0
//  
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// 
using System;
using System.Collections.Generic;
using Xunit.Reporting.Core.Configuration;

namespace Xunit.Reporting.Specs.Core.Configuration
{
    [Concern(typeof (ArgumentMapFactory))]
    public class When_mapping_console_arguments_to_a_one_to_many_map : StaticContextSpecification
    {
        private List<string> consoleArguments;
        private IDictionary<string, ICollection<string>> mapped;
        private ArgumentMapFactory Sut;

        protected override void EstablishContext()
        {
            consoleArguments = new List<string>
            {
                "/Name:John",
                "/Name:Doe",
                "/Age:66"
            };

            Sut = new ArgumentMapFactory(() => consoleArguments);
        }

        protected override void Because()
        {
            mapped = Sut.Create();
        }

        [Observation]
        public void Should_split_the_arguments_based_on_a_slash_and_a_colon()
        {
            mapped["Age"].ShouldOnlyContain("66");
        }

        [Observation]
        public void Should_group_multiple_occurances_of_an_argument_by_the_key()
        {
            mapped["Name"].ShouldOnlyContain("John", "Doe");
        }
    }

    [Concern(typeof (ArgumentMapFactory))]
    public class When_mapping_console_arguments_containing_a_file_path_to_a_one_to_many_map : StaticContextSpecification
    {
        private List<string> consoleArguments;
        private IDictionary<string, ICollection<string>> mapped;
        private ArgumentMapFactory Sut;

        protected override void EstablishContext()
        {
            consoleArguments = new List<string>
            {
                @"/Assembly:C:\\temp\test.txt",
            };

            Sut = new ArgumentMapFactory(() => consoleArguments);
        }

        protected override void Because()
        {
            mapped = Sut.Create();
        }

        [Observation]
        public void Should_be_able_to_extract_the_file_path()
        {
            mapped["Assembly"].ShouldOnlyContain(@"C:\\temp\test.txt");
        }
    }

    [Concern(typeof(ArgumentMapFactory))]
    public class When_mapping_console_arguments_containing_a_file_path_with_spaces_to_a_one_to_many_map : StaticContextSpecification
    {
        private List<string> consoleArguments;
        private IDictionary<string, ICollection<string>> mapped;
        private ArgumentMapFactory Sut;

        protected override void EstablishContext()
        {
            consoleArguments = new List<string>
            {
                @"/Assembly:'C:\\Documents and Settings\test.txt'",
            };

            Sut = new ArgumentMapFactory(() => consoleArguments);
        }

        protected override void Because()
        {
            mapped = Sut.Create();
        }

        [Observation]
        public void Should_be_able_to_extract_the_file_path()
        {
            mapped["Assembly"].ShouldOnlyContain(@"C:\\Documents and Settings\test.txt");
        }
    }

    [Concern(typeof (ArgumentMapFactory))]
    public class When_trying_to_mal_formatted_console_arguments : StaticContextSpecification
    {
        private List<string> consoleArguments;
        private ArgumentMapFactory Sut;
        private Action tryingToMapMallformattedConsoleArguments;

        protected override void EstablishContext()
        {
            consoleArguments = new List<string>
            {
                @"File:C:\\temp\test.txt",
            };

            Sut = new ArgumentMapFactory(() => consoleArguments);
        }

        protected override void Because()
        {
            tryingToMapMallformattedConsoleArguments = () => Sut.Create();
        }

        [Observation]
        public void Should_throw_an__ArgumentExeption__indicating_malformatted_arguments()
        {
            tryingToMapMallformattedConsoleArguments
                .ShouldThrowAn<ArgumentException>()
                .Message
                .ShouldBeEqualTo("Recieved malformatted arguments. Unable to proceed . . .");
        }
    }
}