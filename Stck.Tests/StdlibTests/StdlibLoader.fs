module StdlibTests.StdlibLoader

open System.IO
open System.Reflection
open Stck

let load (stdlib : string) =
    Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)
    |> (fun directory -> Path.Combine(directory, stdlib))
    |> (fun file -> File.ReadAllText(file))
    |> sprintf "```%s```"