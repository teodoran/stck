stck
====
_a stack-based programming language_

stck is is a programming languague inspired by [Forth](https://en.wikipedia.org/wiki/Forth_(programming_language)). Variables are never declared, values is just placed on a global stack. Syntax is minimal. Currently, the only supported data-type is 32-bit integers.

Installation
------------

You'll need a F# compiler (fsharpc/fshapri) to compile the stck interpreter. [This guide](http://fsharp.org/use/linux/) is usefull if you're using Linux. When the compiler is installed, simply run:

    fsharpc stck.fs && ./stck.exe ./euler-one.stck

This should compile the interpreter into stck.exe, and run a stck-program, solving the first [Project Euler](https://projecteuler.net/) problem.

Using the languague
-------------------
