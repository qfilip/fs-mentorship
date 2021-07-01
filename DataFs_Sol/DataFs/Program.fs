open System

open QueryHelpers

let listCourses offset limit =
    sql
        """
        SELECT CourseId, CourseName
          FROM Course
        OFFSET @Offset ROWS
         FETCH
          NEXT @Limit ROWS ONLY
        ;
        """
        [
            p "Offset" offset
            p "Limit" limit
        ]

type Cocktail =
    {
        Id: Guid
        Name: string
    }

[<EntryPoint>]
let main argv =
    DapperConfig.RegisterOptionTypes()
    
    
    let query = listCourses 0 1000
    let connectionString = sprintf "Data Source=%s\\requistador.db3; Version= 3;" __SOURCE_DIRECTORY__
    printfn "%s" connectionString
    
    let cocktailList = AppSql.query<Cocktail> connectionString query |> Async.RunSynchronously
    printfn "%A" cocktailList
    0 // return an integer exit code