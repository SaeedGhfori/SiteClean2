using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Application.Implementations.Validators.Invoices
{
    internal class InvoiceDtoValidator : AbstractValidator<InvoiceDto>
    {
        public InvoiceDtoValidator()
        {
            RuleFor(p => p.Title)
                .NotEmpty().WithMessage("لطفا عنوان را وارد کنید")
                //.NotNull().WithMessage("عنوان خالی است")
                .MinimumLength(9).WithMessage("طول عنوان کم است");

            //RuleFor(p => p.Email)
            //    .NotEmpty()
            //    .EmailAddress();

            //RuleFor(p => p.Age)
            //    .GreaterThan(18)
            //    .LessThan(50);

            //RuleFor(p => p.ListProduct).Must(i=> i?.Count() > 0 & i?.Count()? <= 2);// برای تعداد لیست بیشتر و کمتر نباشد

            //RuleFor(p => p.Price)
            //   .GreaterThan(50000).WithMessage("{PropertyName} must greater than {ComparisonValue}");

        }
    }
}
