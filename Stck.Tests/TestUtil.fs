module TestUtil

open System.IO
open Stck

let stdlibContext =
    File.ReadAllText(stdlib)
    |> sprintf "```%s```"
    |> (fun lib -> eval lib emptyContext)
