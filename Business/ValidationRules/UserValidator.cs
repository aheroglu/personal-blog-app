using Entity.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules
{
    public class UserValidator:AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Kullanıcı adı alanı boş bırakılamaz!");
            RuleFor(x => x.UserName).Length(8, 15).WithMessage("En az 8, en fazla 15 karakter girilmelidir!");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Parola alanı boş bırakılamaz!");
            RuleFor(x => x.Password).Length(8, 15).WithMessage("En az 8, en fazla 15 karakter girilmelidir!");
        }
    }
}
