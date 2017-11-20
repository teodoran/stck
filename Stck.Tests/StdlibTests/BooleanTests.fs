module StdlibTests.BooleanTests

open System.IO
open Expecto
open Stck

let emptyContext = (Heap Map.empty, Empty)
let stdlibFile = File.ReadAllText(Path.Combine("..", "Stck", stdlib))
let stdlibContext = eval (sprintf "```%s```" stdlibFile) emptyContext

[<Tests>]
let tests =
    testList "Boolean tests" [
        testCase "true should be a function returning the first of two arguments" <| fun _ ->
            let expectedStack = Stack (Operation "foo", Empty)
            let _, actualStack = (eval "foo bar true app" stdlibContext)
            
            Expect.equal actualStack expectedStack "foo bar true app should return foo"

        testCase "false should be a function returning the second of two arguments" <| fun _ ->
            let expectedStack = Stack (Operation "bar", Empty)
            let _, actualStack = (eval "foo bar false app" stdlibContext)
            
            Expect.equal actualStack expectedStack "foo bar false app should return bar"

        testCase "not should make true behave as false" <| fun _ ->
            let expectedStack = Stack (Operation "bar", Empty)
            let _, actualStack = (eval "foo bar true not app" stdlibContext)
            
            Expect.equal actualStack expectedStack "foo bar true not app should return bar"

        testCase "not should make false behave as true" <| fun _ ->
            let expectedStack = Stack (Operation "foo", Empty)
            let _, actualStack = (eval "foo bar false not app" stdlibContext)
            
            Expect.equal actualStack expectedStack "foo bar false not app should return foo"

        testCase "true should be equivalent (<=>) with true" <| fun _ ->
            let expectedStack = Stack (Operation "foo", Empty)
            let _, actualStack = (eval "foo bar true true <=> app" stdlibContext)
            
            Expect.equal actualStack expectedStack "foo bar true true <=> app should return foo"

        testCase "false should be equivalent (<=>) with false" <| fun _ ->
            let expectedStack = Stack (Operation "foo", Empty)
            let _, actualStack = (eval "foo bar false false <=> app" stdlibContext)
            
            Expect.equal actualStack expectedStack "foo bar false false <=> app should return foo"

        testCase "? (if/else) should apply the first of two quotations if given true" <| fun _ ->
            let expectedStack = Stack (Operation "foo", Empty)
            let _, actualStack = (eval "true [foo] [bar] ?" stdlibContext)
            
            Expect.equal actualStack expectedStack "true [foo] [bar] ? should return foo"

        testCase "? (if/else) should apply the second of two quotations if given false" <| fun _ ->
            let expectedStack = Stack (Operation "bar", Empty)
            let _, actualStack = (eval "false [foo] [bar] ?" stdlibContext)
            
            Expect.equal actualStack expectedStack "false [foo] [bar] ? should return bar"

        testCase "not should work with ? (if/else)" <| fun _ ->
            let expectedStack = Stack (Operation "foo", Empty)
            let _, actualStack = (eval "false not [foo] [bar] ?" stdlibContext)
            
            Expect.equal actualStack expectedStack "false not [foo] [bar] ? should return foo"
    ]