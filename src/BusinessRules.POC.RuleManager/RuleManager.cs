using System;
using System.Collections.Generic;
using Autofac;
using BusinessRules.POC.Configuration;
using BusinessRules.POC.Enums;
using BusinessRules.POC.Interfaces;
using BusinessRules.POC.Models;
using BusinessRules.POC.RuleLearnDelFAMType66.ExclusionRules;

namespace BusinessRules.POC.RuleManager
{
    public class RuleManager : IDisposable
    {
        private IContainer _container;

        private ILifetimeScope _scope;

        public RuleManager()
        {
        }

        public RuleManager(IContainer container)
        {
            _container = container;
        }

        public void ExecuteRules()
        {
            if(_container == null) ConfigureIoCContainer();

            var item3 = _scope.Resolve<ILearnerDelFam66ExclusionRule>();
            var item = _scope.Resolve<IEnumerable<IRule<Learner>>>();
            var item2 = _scope.ResolveKeyed<IRule<Learner>>(RuleNames.LearnDelFam66);
        }

        private void ConfigureIoCContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<BusinessLogicAutofacModule>();
            _container = builder.Build();
            _scope = _container.BeginLifetimeScope();
        }

        public void Dispose()
        {
            _container?.Dispose();
            _scope?.Dispose();
        }
    }
}
