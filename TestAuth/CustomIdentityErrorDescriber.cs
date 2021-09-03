using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestAuth
{
    public class CustomIdentityErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError DefaultError() 
        {
            return new IdentityError 
            { 
                Code = nameof(DefaultError), Description = $"Непредвиденная ошибка" 
            }; 
        }
        public override IdentityError ConcurrencyFailure() 
        { 
            return new IdentityError 
            { 
                Code = nameof(ConcurrencyFailure), Description = "Объект был изменен"
            };
        }
        public override IdentityError PasswordMismatch() 
        { 
            return new IdentityError 
            { 
                Code = nameof(PasswordMismatch), Description = "Неверный пароль" 
            };
        }
        public override IdentityError InvalidToken() 
        { 
            return new IdentityError 
            { 
                Code = nameof(InvalidToken), Description = "Неверный токен" 
            };
        }
        public override IdentityError LoginAlreadyAssociated() 
        { 
            return new IdentityError 
            { 
                Code = nameof(LoginAlreadyAssociated), Description = "Пользователь с таким логином уже существует" 
            };
        }
        public override IdentityError InvalidUserName(string userName) 
        { 
            return new IdentityError 
            { 
                Code = nameof(InvalidUserName), Description = $"Имя пользователя '{userName}' является неверным, так так оно может состоять только из букв и (или) цифр"
            };
        }
        public override IdentityError InvalidEmail(string email) 
        { 
            return new IdentityError 
            { 
                Code = nameof(InvalidEmail), Description = $"Email '{email}' является неверным" 
            };
        }
        public override IdentityError DuplicateUserName(string userName) 
        { 
            return new IdentityError 
            { 
                Code = nameof(DuplicateUserName), Description = $"Имя пользователя '{userName}' уже занято" 
            };
        }
        public override IdentityError DuplicateEmail(string email) 
        { 
            return new IdentityError 
            { 
                Code = nameof(DuplicateEmail), Description = $"Email '{email}' уже занят" 
            };
        }
        public override IdentityError InvalidRoleName(string role) 
        { 
            return new IdentityError 
            { 
                Code = nameof(InvalidRoleName), Description = $"Имя роли '{role}' уже существует" 
            }; 
        }
        public override IdentityError DuplicateRoleName(string role)
        { 
            return new IdentityError 
            { 
                Code = nameof(DuplicateRoleName), Description = $"Имя роли '{role}' уже занято" 
            }; 
        }
        public override IdentityError UserAlreadyHasPassword() 
        { 
            return new IdentityError 
            { 
                Code = nameof(UserAlreadyHasPassword), Description = "У пользователя уже установлен пароль" 
            };
        }
        public override IdentityError UserLockoutNotEnabled() 
        { 
            return new IdentityError 
            { 
                Code = nameof(UserLockoutNotEnabled), Description = "Для этого пользователя отключена блокировка" 
            };
        }
        public override IdentityError UserAlreadyInRole(string role) 
        { 
            return new IdentityError 
            { 
                Code = nameof(UserAlreadyInRole), Description = $"Пользователь уже имеет роль '{role}'" 
            };
        }
        public override IdentityError UserNotInRole(string role) 
        { 
            return new IdentityError 
            { 
                Code = nameof(UserNotInRole), Description = $"Пользователь не имеет роли '{role}'" 
            };
        }
        public override IdentityError PasswordTooShort(int length) 
        { 
            return new IdentityError 
            { 
                Code = nameof(PasswordTooShort), Description = $"Пароль должен содержать не менее {length} символов" 
            };
        }
        public override IdentityError PasswordRequiresNonAlphanumeric() 
        { 
            return new IdentityError 
            { 
                Code = nameof(PasswordRequiresNonAlphanumeric), Description = "Пароль должен содержать хотя бы один не буквенно-цифровой символ" 
            };
        }
        public override IdentityError PasswordRequiresDigit() 
        { 
            return new IdentityError 
            { 
                Code = nameof(PasswordRequiresDigit), Description = "Пароль должен содержать хотя бы одну цифру ('0'-'9')"
            };
        }
        public override IdentityError PasswordRequiresLower() 
        { 
            return new IdentityError 
            { 
                Code = nameof(PasswordRequiresLower), Description = "Пароль должен содержать хотя бы одну строчную букву ('a'-'z')" 
            };
        }
        public override IdentityError PasswordRequiresUpper() 
        { 
            return new IdentityError 
            { 
                Code = nameof(PasswordRequiresUpper), Description = "Пароль должен содержать хотя бы одну заглавную букву ('A'-'Z')"
            };
        }
    }
}
