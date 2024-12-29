﻿namespace TheGrace.Domain.Exceptions.Commons;

public class DomainException : Exception
{
    public string Title { get; }

    protected DomainException(string title, string message) 
        : base(message) => Title = title;
}