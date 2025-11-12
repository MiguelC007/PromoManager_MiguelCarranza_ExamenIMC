using System;
namespace PromoManager.Domain.Exceptions;
public class DomainException : Exception {
    public DomainException(string message) : base(message) { }
}