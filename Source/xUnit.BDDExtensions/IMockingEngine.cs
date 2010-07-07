// Copyright 2010 Bj�rn Rochel - http://www.bjro.de/ 
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
using System.Linq.Expressions;

namespace Xunit
{
    /// <summary>
    /// Interface to a mocking library. 
    /// </summary>
    public interface IMockingEngine
    {
        /// <summary>
        /// Creates a dependency of the type specified via <paramref name="interfaceType"/>.
        /// </summary>
        /// <param name="interfaceType">
        /// Specifies the interface type to create a dependency for.
        /// </param>
        /// <returns>
        /// The created dependency instance.
        /// </returns>
        object Stub(Type interfaceType);

        /// <summary>
        /// Creates a partial mock.
        /// </summary>
        /// <typeparam name="T">
        /// Specifies the type of the partial mock. This needs to be 
        /// an abstract base class.
        /// </typeparam>
        /// <param name="args">
        /// Specifies the constructor parameters.
        /// </param>
        /// <returns>
        /// The created instance.
        /// </returns>
        T PartialMock<T>(params  object[] args) where T : class;

        /// <summary>
        ///   Configures the behavior of the dependency specified by <paramref name = "dependency" />.
        /// </summary>
        /// <typeparam name = "TDependency">
        ///   Specifies the type of the dependency.
        /// </typeparam>
        /// <typeparam name = "TReturnValue">
        ///   Specifies the type of the return value.
        /// </typeparam>
        /// <param name = "dependency">
        ///   The dependency to configure behavior on.
        /// </param>
        /// <param name = "func">
        ///   Expression to set up the behavior.
        /// </param>
        /// <returns>
        ///   A <see cref = "IQueryOptions{TReturn}" /> for further configuration.
        /// </returns>
        IQueryOptions<TReturnValue> SetUpQueryBehaviorFor<TDependency, TReturnValue>(
            TDependency dependency, 
            Expression<Func<TDependency, TReturnValue>> func) where TDependency : class;

        /// <summary>
        ///   Configures the behavior of the dependency specified by <paramref name = "dependency" />.
        /// </summary>
        /// <typeparam name = "TDependency">
        ///   Specifies the type of the dependency.
        /// </typeparam>
        /// <param name = "dependency">
        ///   The dependency to configure behavior on.
        /// </param>
        /// <param name = "func">
        ///   Configures the behavior. This must be a void method.
        /// </param>
        /// <returns>
        ///   A <see cref = "ICommandOptions" /> for further configuration.
        /// </returns>
        /// <remarks>
        ///   This method is used for command, e.g. methods returning void.
        /// </remarks>
        ICommandOptions SetUpCommandBehaviorFor<TDependency>(
            TDependency dependency,
            Expression<Action<TDependency>> func) where TDependency : class;
    }
}