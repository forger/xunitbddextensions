﻿// Copyright 2009 Björn Rochel - http://www.bjro.de/ 
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
using StructureMap.Configuration.DSL;
using Xunit.Reporting.Core.Configuration;
using Xunit.Reporting.Core.Generator;

namespace Xunit.Reporting
{
    /// <summary>
    /// A registry class for all initialization work, which can't be performed 
    /// automatically by StructureMap.
    /// </summary>
    public class EngineRegistry : Registry
    {
        /// <summary>
        /// Performs the configuration of the IoC
        /// </summary>
        public EngineRegistry()
        {
                        Scan(scanner =>
            {
                scanner.Assembly(typeof (IArgumentMap).Assembly);
                scanner.WithDefaultConventions();
                scanner.With(new GeneratorConvensions());
            });

            ForRequestedType<IArgumentMap>()
                .TheDefault.Is.ConstructedBy(ctor => ctor.GetInstance<ArgumentMapFactory>().Create());

            ForRequestedType<IReportGenerator>()
                .MissingNamedInstanceIs.ConstructedBy(exp => exp.GetInstance<IReportGenerator>("Html"));

            ForRequestedType<IReportGenerator>()
                .TheDefault.Is.ConstructedBy(ctorExpression =>
                {
                    var properties = ctorExpression.GetInstance<IArguments>();
                    var generator = properties.Get(ArgumentKeys.Generator);
                    var nameOfTargetGenerator = string.IsNullOrEmpty(generator) ? "Html" : generator;
                    return ctorExpression.GetInstance<IReportGenerator>(nameOfTargetGenerator);
                });

            ForRequestedType<Func<IEnumerable<string>>>()
                .TheDefault.Is.ConstructedBy(ctor => () => DropProcessName(Environment.GetCommandLineArgs()));
        }

        private static List<string> DropProcessName(string[] commandLineArgs)
        {
            var argsWithoutProcessName = new List<string>();

            for (var index = 1; index < commandLineArgs.Length; index++)
            {
                argsWithoutProcessName.Add(commandLineArgs[index]);
            }

            return argsWithoutProcessName;
        }
    }
}
