module Mapper

open Models
open Dtos
open WrappedTypes
open Builders

let toDto (x: CrewMember) =
    let dto: CrewMemberDto = {
        Name = x.Name
        YrsOfExp = x.YrsOfExp |> YrsOfExp.unwrap
        Role = x.Role
        Plane = x.Plane
    }
    dto


let toModel (x: CrewMemberDto) =
    let result = ResultBuilder()
    result {
        let! yrsOfExp = YrsOfExp.wrap x.YrsOfExp
        let! 
    }