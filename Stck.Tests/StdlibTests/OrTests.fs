module StdlibTests.OrTests

open System.IO
open System.Reflection
open Expecto
open Stck

let emptyContext = (Heap Map.empty, Empty)
let stdlibFile = 
    File.ReadAllText(
        Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), stdlib))
let stdlibContext = eval (sprintf "```%s```" stdlibFile) emptyContext

[<Tests>]
let tests =
    testList "or tests" [
        testCase "true true or should be true #1" <| fun _ ->
            let expectedStack = Stack (Operation "foo", Empty)
            let _, actualStack = (eval "true true or [foo] [bar] ?" stdlibContext)
            
            Expect.equal actualStack expectedStack "true true or [foo] [bar] ? should return foo"

        testCase "true true or should be true #2" <| fun _ ->
            let expectedStack = Stack (Operation "foo", Empty)
            let _, actualStack = (eval "foo bar true true or app" stdlibContext)
            
            Expect.equal actualStack expectedStack "foo bar true true or app should return foo"

        testCase "false true or should be true #1" <| fun _ ->
            let expectedStack = Stack (Operation "foo", Empty)
            let _, actualStack = (eval "false true or [foo] [bar] ?" stdlibContext)
            
            Expect.equal actualStack expectedStack "false true or [foo] [bar] ? should return foo"

        testCase "false true or should be true #2" <| fun _ ->
            let expectedStack = Stack (Operation "foo", Empty)
            let _, actualStack = (eval "foo bar false true or app" stdlibContext)
            
            Expect.equal actualStack expectedStack "foo bar false true or app should return foo"

        testCase "true false or should be true #1" <| fun _ ->
            let expectedStack = Stack (Operation "foo", Empty)
            let _, actualStack = (eval "true false or [foo] [bar] ?" stdlibContext)
            
            Expect.equal actualStack expectedStack "true false or [foo] [bar] ? should return foo"

        testCase "true false or should be true #2" <| fun _ ->
            let expectedStack = Stack (Operation "foo", Empty)
            let _, actualStack = (eval "foo bar true false or app" stdlibContext)
            
            Expect.equal actualStack expectedStack "foo bar true false or app should return foo"

        testCase "false false or should be false #1" <| fun _ ->
            let expectedStack = Stack (Operation "bar", Empty)
            let _, actualStack = (eval "false false or [foo] [bar] ?" stdlibContext)
            
            Expect.equal actualStack expectedStack "false false or [foo] [bar] ? should return bar"

        testCase "false false or should be false #2" <| fun _ ->
            let expectedStack = Stack (Operation "bar", Empty)
            let _, actualStack = (eval "foo bar false false or app" stdlibContext)
            
            Expect.equal actualStack expectedStack "foo bar false false or app should return bar"
    ]