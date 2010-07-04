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
using System.Linq;
using Xunit.Reporting.Core;

namespace Xunit.Reporting.Specs.Core
{
    [Concern(typeof (Observation))]
    public class When_building_observations_from_a_valid_context_type : StaticContextSpecification
    {
        private IEnumerable<Observation> createdObservations;

        protected override void Because()
        {
            createdObservations = Observation.BuildAllFrom(GetType());
        }

        [Observation]
        public void Should_create_observations_for_all_public_methods_marked_with_the__ObservationAttribute__()
        {
            createdObservations.Count().ShouldBeEqualTo(1);
        }
    }

    [Concern(typeof(Observation))]
    public class When_building_observations_from_a_valid_derviced_type : StaticContextSpecification
    {
        private IEnumerable<Observation> createdObservations;

        protected override void Because()
        {
            createdObservations = Observation.BuildAllFrom(typeof(Foo_bar_derived_concern));
        }

        [Observation]
        public void Should_create_observations_for_all_public_methods_marked_with_the__ObservationAttribute__()
        {
            createdObservations.Count().ShouldBeEqualTo(2);
        }
    }

    [Concern(typeof (Observation))]
    public class When_building_observations_from_a_type_without_marked_methods : StaticContextSpecification
    {
        private IEnumerable<Observation> createdObservations;

        protected override void Because()
        {
            createdObservations = Observation.BuildAllFrom(typeof (string));
        }

        [Observation]
        public void Should_create_an_empty_collection()
        {
            createdObservations.Count().ShouldBeEqualTo(0);
        }
    }

    [Concern(typeof (Observation))]
    public class After_creating_a_readable_representation_of_an__Observation__ : StaticContextSpecification
    {
        private Observation observation;
        private string readableRepresentation;

        protected override void EstablishContext()
        {
            observation = Observation.BuildAllFrom(
                typeof (After_this__fake__specification_has_been_executed))
                .First();
        }

        protected override void Because()
        {
            readableRepresentation = observation.ToString();
        }

        [Observation]
        public void Should_have_lower_cased_the_first_letter()
        {
            readableRepresentation[0].ShouldBeEqualTo('i');
        }

        [Observation]
        public void Should_have_replaced_underscores_with_spaces()
        {
            readableRepresentation.ShouldNotContain('_');
            readableRepresentation.ShouldContain(' ');
        }

        [Observation]
        public void Should_have_replaced_double_underscores_with_double_quotes()
        {
            readableRepresentation.ShouldContain("\"nothing\"");
        }

        [Observation]
        public void Should_have_created_the_expected_text()
        {
            readableRepresentation.ShouldBeEqualTo("it should effectively do \"nothing\"");
        }
    }
}