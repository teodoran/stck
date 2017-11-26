module StdlibTests.NumeralTests

open System.IO
open Xunit
open FsUnit.Xunit
open Stck

let emptyContext = (Heap Map.empty, Empty)
let stdlibContext =
    File.ReadAllText(stdlib)
    |> sprintf "```%s```"
    |> (fun lib -> eval lib emptyContext)

[<Fact>]
let ``0 should apply a function zero times`` () =
    let expectedStack = Stack (Operation "x", Empty)
    let _, actualStack = (eval "x [f] 0 app" stdlibContext)
    
    actualStack |> should equal expectedStack

[<Fact>]
let ``1 should apply a function one time`` () =
    let expectedStack = Stack (Operation "f", Stack (Operation "x", Empty))
    let _, actualStack = (eval "x [f] 1 app" stdlibContext)
    
    actualStack |> should equal expectedStack

[<Fact>]
let ``2 should apply a function twice`` () =
    let expectedStack = Stack (Operation "f", Stack (Operation "f", Stack (Operation "x", Empty)))
    let _, actualStack = (eval "x [f] 2 app" stdlibContext)
    
    actualStack |> should equal expectedStack

[<Fact>]
let ``inc should increment a numeral`` () =
    let expectedStack = Stack (Operation "f", Stack (Operation "x", Empty))
    let _, actualStack = (eval "x [f] 0 inc app" stdlibContext)
    
    actualStack |> should equal expectedStack

[<Fact>]
let ``inc should increment a numeral twice`` () =
    let expectedStack = Stack (Operation "f", Stack (Operation "f", Stack (Operation "x", Empty)))
    let _, actualStack = (eval "x [f] 0 inc inc app" stdlibContext)
    
    actualStack |> should equal expectedStack

[<Fact>]
let ``2 multiplied with 3 should be 6`` () =
    let expectedStack = Stack (Operation "f", Stack (Operation "f", Stack (Operation "f", Stack (Operation "f", Stack (Operation "f", Stack (Operation "f", Stack (Operation "x", Empty)))))))
    let _, actualStack = (eval "x [f] 2 3 * app" stdlibContext)
    
    actualStack |> should equal expectedStack

[<Fact>]
let ``0 multiplied with 3 should be 0`` () =
    let expectedStack = Stack (Operation "x", Empty)
    let _, actualStack = (eval "x [f] 0 3 * app" stdlibContext)
    
    actualStack |> should equal expectedStack

[<Fact>]
let ``2 added to 3 should be 5`` () =
    let expectedStack = Stack (Operation "f", Stack (Operation "f", Stack (Operation "f", Stack (Operation "f", Stack (Operation "f", Stack (Operation "x", Empty))))))
    let _, actualStack = (eval "x [f] 2 3 + app" stdlibContext)
    
    actualStack |> should equal expectedStack

[<Fact>]
let ``0 added to 3 should be 3`` () =
    let expectedStack = Stack (Operation "f", Stack (Operation "f", Stack (Operation "f", Stack (Operation "x", Empty))))
    let _, actualStack = (eval "x [f] 0 3 + app" stdlibContext)
    
    actualStack |> should equal expectedStack