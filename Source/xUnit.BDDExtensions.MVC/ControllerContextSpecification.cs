﻿// Copyright 2010 Albert Weinert - http://der-albert.com
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
using System.Web.Mvc;

namespace Xunit
{
    public abstract class ControllerContextSpecification<T> : InstanceContextSpecification<T>
        where T : Controller
    {
        protected ActionResult Result;
        private ControllerInvokerBuilder invokerBuilder;

        protected ViewResult ViewResult
        {
            get { return (ViewResult) Result; }
        }

        protected override void PrepareSut()
        {
            base.PrepareSut();
            invokerBuilder = CreateTestActionInvoker();
        }

        protected virtual ControllerInvokerBuilder CreateTestActionInvoker()
        {
            return new ControllerInvokerBuilder(this, new TestControllerActionInvoker());
        }

        protected ControllerInvokerBuilder InvokeAction<TResult>(Expression<Func<T, TResult>> expression)
        {
            invokerBuilder.Controller(Sut).Action(expression);
            return invokerBuilder;
        }


        protected ControllerInvokerBuilder InvokePostAction<TResult>(Expression<Func<T, TResult>> expression)
        {
            invokerBuilder.Controller(Sut).Action(expression).RequestContext.HttpMethod("POST").AntiForgeryToken();
            return invokerBuilder;
        }
    }
}