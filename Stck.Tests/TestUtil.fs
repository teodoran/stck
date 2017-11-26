module TestUtil

open System.IO
open Stck

let emptyContext = (Heap Map.empty, Empty)

let stdlibContext =
    File.ReadAllText(stdlib)
    |> sprintf "```%s```"
    |> (fun lib -> eval lib emptyContext)

let rec strs = function
    | Empty -> ""
    | Stack (e, Empty) -> stre e
    | Stack (e, r) -> sprintf "%s %s" (strs r) (stre e)
and stre = function
    | Operation w -> w
    | Quotation q -> sprintf "[%s]" (strs q)
    | Exception e ->
        match e with
        | StackUnderflow -> "Exception: StackUnderflow"
        | MissingQuotation -> "Exception: MissingQuotation"
        | Failure s -> sprintf "Exception: %s" s