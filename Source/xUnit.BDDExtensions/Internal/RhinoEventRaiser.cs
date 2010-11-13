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

namespace Xunit.Internal
{
    public class RhinoEventRaiser : IEventRaiser
    {
        private readonly Rhino.Mocks.Interfaces.IEventRaiser _rhinoEventRaiser;

        public RhinoEventRaiser(Rhino.Mocks.Interfaces.IEventRaiser rhinoEventRaiser)
        {
            _rhinoEventRaiser = rhinoEventRaiser;
        }

        #region IEventRaiser Members

        public void Raise(object sender, EventArgs e)
        {
            _rhinoEventRaiser.Raise(sender, e);
        }

        public void Raise(params object[] args)
        {
            _rhinoEventRaiser.Raise(args);
        }

        #endregion
    }
}