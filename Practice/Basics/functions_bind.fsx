// Main Source: https://fsharpforfunandprofit.com/posts/elevated-world-2/

type E<'a> =
    | Is of 'a
    | Isnt


// description: lifts a function to eWorld
// note: use List.collect for lists
let bind f xElev =
    match xElev with
    | Is x -> f x
    | _ -> Isnt


let (>>=) = bind