//  Copyright 2010 xUnit.BDDExtensions
//    
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License. 
//  You may obtain a copy of the License at
//    
//        http://www.apache.org/licenses/LICENSE-2.0
//    
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or
//  implied. See the License for the specific language governing permissions and
//  limitations under the License.  
//  
using System;

namespace Xunit
{
    /// <summary>
    ///   Helper base class for implementing <see cref = "IBehaviorConfig" /> interface.
    /// </summary>
    public abstract class BehaviorConfigBase : IBehaviorConfig
    {
        #region IBehaviorConfig Members

        /// <summary>
        ///   Uses the accessor specified by <paramref name = "accessor" /> in order to configure some behavior on dependencies
        ///   of the system under test. This is called before the system under test has been created.
        /// </summary>
        /// <param name = "accessor">Specifies the accessor for accessing dependencies of the SUT.</param>
        public virtual void EstablishContext(IFakeAccessor accessor)
        {
        }

        /// <summary>
        ///   Does some preparation of the sut itself (e.g. adding some elements to container like structures)
        /// </summary>
        /// <param name = "sut">Specifies the sut.</param>
        public virtual void PrepareSut(object sut)
        {
        }

        /// <summary>
        ///   Performs some cleanup operation on the sut.
        /// </summary>
        /// <param name = "sut">Specifies the sut.</param>
        public virtual void Cleanup(object sut)
        {
        }

        #endregion
    }

    /// <summary>
    ///   Helper base class for implementing <see cref = "IBehaviorConfig" />.
    /// </summary>
    public abstract class BehaviorConfigBase<T> : IBehaviorConfig
    {
        public virtual void EstablishContext(IFakeAccessor accessor)
        {
        }

        void IBehaviorConfig.PrepareSut(object sut)
        {
            PrepareSut((T) sut);
        }

        protected virtual void PrepareSut(T sut)
        {
        }

        void IBehaviorConfig.Cleanup(object sut)
        {
            Cleanup((T) sut);
        }

        protected virtual void Cleanup(T sut)
        {
        }
    }
}
