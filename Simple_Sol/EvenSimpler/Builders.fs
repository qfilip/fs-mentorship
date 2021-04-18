module Builders

type ResultBuilder() =
    
    member _.Bind(x,f) = Result.bind f x
    
    member _.Return(x) = Ok x