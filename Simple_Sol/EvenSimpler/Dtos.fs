module Dtos

open Models

type CrewMemberDto = {
    Name: string
    YrsOfExp: int
    Role: Role
    Plane: Plane
}