using FluentValidation;
using ProceedLabs.Models.ApiModels.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProceedLabs.Models.ApiModels.Validators
{
    public class CreateFlowRequestValidator: AbstractValidator<CreateFlowRequest>
    {
        public CreateFlowRequestValidator()
        {
            RuleFor(r => r.Name).NotEmpty();
            RuleFor(r => r.States).NotEmpty();
            RuleFor(r => r.States).Must(x => x.Count >= 1)
                .WithMessage("Flow must contains at least one state")
                .When(x => x.States != null);
            RuleForEach(r=>r.States)
                .SetValidator(new FlowStateRequestValidator())
                .When(x => x.States != null);

        }
    }

    public class FlowStateRequestValidator : AbstractValidator<FlowStateRequest>
    {
        public FlowStateRequestValidator()
        {
            RuleFor(r => r.StateId).NotEmpty();
            RuleFor(r => r.Order).NotEmpty()
                .GreaterThan(0)
                .WithMessage("State order cannot be 0");
            
        }
    }
}
