type FunctionType<'a> =
    | IntResult of ('a -> int)
    | BoolResult of ('a -> bool)
    | GenericResult of ('a -> 'a)