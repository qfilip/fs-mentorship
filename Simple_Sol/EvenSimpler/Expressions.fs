module Expressions

type ValidatorExpression() =
    member _.Bind(x, f) = ValidationCommon.bind f x
    member _.Return(x) = ValidationCommon.Result.Success x