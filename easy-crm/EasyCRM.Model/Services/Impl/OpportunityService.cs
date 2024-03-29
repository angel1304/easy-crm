﻿using System.Collections.Generic;
using EasyCRM.Model.Domains;
using EasyCRM.Model.Repositories;
using EasyCRM.Model.Repositories.Entity;
using System.Linq.Expressions;
using System;

namespace EasyCRM.Model.Services.Impl
{
    public class OpportunityService : IOpportunityService
    {
        private IValidationDictionary _validationDictionary;
        private IOpportunityRepository _repository;


        public OpportunityService(IValidationDictionary validationDictionary)
            : this(validationDictionary, new OpportunityEntityRepository())
        { }


        public OpportunityService(IValidationDictionary validationDictionary, IOpportunityRepository repository)
        {
            _validationDictionary = validationDictionary;
            _repository = repository;
        }


        public bool ValidateOpportunity(Opportunity opportunityToValidate)
        {
            //convert null values to empty strings
            opportunityToValidate.Description = opportunityToValidate.Description ?? "";

            if (opportunityToValidate.Amount <= 0)
                _validationDictionary.AddError("Opportunity.Amount", "Amount must be greater than 0.");
            if (opportunityToValidate.Description.Trim().Length == 0)
                _validationDictionary.AddError("Opportunity.Description", "Description is required.");
            return _validationDictionary.IsValid;
        }


        #region IOpportunityService Members

        public bool CreateOpportunity(Opportunity opportunityToCreate)
        {
            // Validation logic
            if (!ValidateOpportunity(opportunityToCreate))
                return false;

            // Database logic
            try
            {
                _repository.Create(opportunityToCreate);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool EditOpportunity(Opportunity opportunityToEdit)
        {
            // Validation logic
            if (!ValidateOpportunity(opportunityToEdit))
                return false;

            // Database logic
            try
            {
                _repository.Update(opportunityToEdit);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool DeleteOpportunity(Opportunity opportunityToDelete)
        {
            try
            {
                _repository.Delete(opportunityToDelete);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public Opportunity GetOpportunity(int id)
        {
            try
            {
                return _repository.Get(id);
            }
            catch
            {
                return null;
            }
        }

        public IEnumerable<Opportunity> ListOpportunities()
        {
            return _repository.ListAll();
        }

        public IEnumerable<Opportunity> ListOpportunitiesByCriteria(Expression<Func<Opportunity, bool>> predicate)
        {
            return _repository.ListAllByCriteria(predicate);
        }
        #endregion

    }
}
