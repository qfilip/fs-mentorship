module Expressions

type ValidatorExpression() =
    member _.Bind(x, f) = Result.bind f x
    member _.Return(x) = Result.Ok x