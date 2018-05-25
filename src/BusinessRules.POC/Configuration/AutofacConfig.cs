using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac.Features.AttributeFilters;
using BusinessRules.POC.Enums;
using BusinessRules.POC.ExternalData;
using BusinessRules.POC.Helpers;
using BusinessRules.POC.Interfaces;
using BusinessRules.POC.Models;
using BusinessRules.POC.ReferenceData;
using BusinessRules.POC.RuleLearnDelFAMType66;
using BusinessRules.POC.RuleLearnDelFAMType66.ExclusionRules;
using BusinessRules.POC.RuleR105;
using BusinessRules.POC.SharedRules;
using BusinessRules.POC.SharedRules.DD28;
using BusinessRules.POC.SharedRules.DD29;
using Module = Autofac.Module;

namespace BusinessRules.POC.Configuration
{
    public class BusinessLogicAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //register local modules here.

            var currentAssembly = Assembly.GetExecutingAssembly();

            //register all implementations of R105SubRule 
            //builder.RegisterType<R105PickLdFamActTypes>().As<IR105PickLdFamACTTypes>().SingleInstance();
            //builder.RegisterType<LearningDeliveryNoOverlappingDatesRule>().As<ILearningDeliveryNoOverlappingDatesRule>()
            //    .SingleInstance();
            //builder.RegisterType<PickValidLdsWithAgeLimitFamTypeAndCode>().As<IPickValidLdsWithAgeLimitFamTypeAndCode>()
            //    .SingleInstance();
            //builder.RegisterType<FetchSpecificFundModelsLDsWithLearnStartDate>()
            //    .As<IFetchSpecificFundModelsLDsWithLearnStartDate>()
            //    .SingleInstance();
            //builder.RegisterType<LearnerDelFamExclusionRulesValidator>().As<ILearnerDelFam66ExclusionRule>()
            //    .SingleInstance();
            //builder.RegisterType<DateHelper>().As<IDateHelper>()
            //    .SingleInstance();
            //builder.RegisterType<ValidateLARNotionalNVQLevelRule>().As<IValidateLARNotionalNVQLevelRule>()
            //    .SingleInstance();

            builder.RegisterType<DD28PickMatchingEmpRecord>().As<IShortRule<DD28SubModel, LearnerEmploymentStatus>>()
                .SingleInstance();

            builder.RegisterAssemblyTypes(currentAssembly)
                .Where(t => t.IsClass)
                .AsImplementedInterfaces()
                .Except<IRule<Learner>>()
                .Except<ISharedRule<Learner, string>>()
                .Except<ISharedRule<List<LearningDelivery>, string>>()
                .Except<ILearnerDelFam66ExclusionRuleForFam>()
                .SingleInstance();







            //regsiter all implementations of IReferenceData


            builder
                .RegisterAssemblyTypes(currentAssembly)
                .AssignableTo<IReferenceData<string,string>>()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder
                .RegisterAssemblyTypes(currentAssembly)
                .AssignableTo<IExternalData<string, List<string>>>()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder
                .RegisterAssemblyTypes(currentAssembly)
                .AssignableTo<IDD28RuleCriteria>()
                .AsImplementedInterfaces()
                .SingleInstance();
            //          builder
            //                .RegisterAssemblyTypes(currentAssembly)
            //                .AssignableTo<IRule<Learner>>()
            //                .AsImplementedInterfaces()
            //                .WithAttributeFiltering()
            //                .SingleInstance();

            //register all rules by rule name. So that they can be explicitly resolved using
            // the following : var r = container.ResolveKeyed<IRule<Learner>>(RuleNames.LearnDelFam66);
            // or via attributes [KeyFilter("")]
            builder
                .RegisterType<LearnDelFAMType66Validator>().Keyed<IRule<Learner>>(RuleNames.LearnDelFam66)
                .WithAttributeFiltering(); 

            builder.RegisterType<R105Validator>().Keyed<IRule<Learner>>(RuleNames.R105);

            builder.RegisterType<LearnerDoBShouldNotBeNullRule>()
                .Keyed<ISharedRule<Learner, bool>>(SharedRuleNames.LearnerDobShouldNotBeNull).SingleInstance();


            builder.RegisterType<DD29Rule>()
                .Keyed<ISharedRule<Learner, string>>(SharedRuleNames.DD29).SingleInstance();
            builder.RegisterType<DD04Rule>()
                .Keyed<ISharedRule<List<LearningDelivery>, List<DD04Result>>>(SharedRuleNames.DD04).SingleInstance();
            builder.RegisterType<DD07Rule>()
                .Keyed<ISharedRule<LearningDelivery, string>>(SharedRuleNames.DD07).SingleInstance();
            builder.RegisterType<DD28Rule>()
                .Keyed<ISharedRule<Learner, string>>(SharedRuleNames.DD28).SingleInstance();
         

        }


    }
}
