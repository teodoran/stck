STCK (Ash edition)
==================
_a stack-based programming language_

![STCK logo](logo.png)

[![Build status](https://ci.appveyor.com/api/projects/status/iapgn0af3k9kw94u/branch/master?svg=true)](https://ci.appveyor.com/project/teodoran/stck/branch/master) [![codecov](https://codecov.io/gh/teodoran/stck/branch/master/graph/badge.svg)](https://codecov.io/gh/teodoran/stck)

### [Try STCK in the browser](https://teodoran.github.io/try-stck)

_"INTERCAL allows only 2 different types of variables, the 16-bit integer and the 32-bit integer." - From the [INTERCAL Programming Language Revised Reference Manual](http://www.muppetlabs.com/~breadbox/intercal-man/s03.html)_

STCK (pronounced stick) is is a programming language inspired by [Forth](https://en.wikipedia.org/wiki/Forth_(programming_language)). Variables are never declared, values is just placed on a global stack. Syntax is minimal.

The only supported data-type used to be 32-bit integers, but now we handle all that with [Church encoding](https://en.wikipedia.org/wiki/Church_encoding).


Installation
------------

You'll need dotnet core 2.0 to compile the STCK interpreter. You'll probably struggle a bit to get everything working on Linux. Google is your friend.

When the compiler is installed, navigate to the folder `/Stck.Console` and run:

    dotnet restore
    dotnet build
    dotnet run

This should compile and launch the interactive interpreter. Execute `quit` to get out.


Using the language
------------------

_"Beware of the Turing tar-pit in which everything is possible but nothing of interest is easy." - [Alan J. Perlis](http://pu.inf.uni-tuebingen.de/users/klaeren/epigrams.html)_

**Pushing symbols**

Write a word, any word, and press enter. You now have a symbol on the stack.

    $> any-word
    [any-word]

Write another one, and then another.

    $> another-one and-then-another
    [any-word another-one and-then-another]

You now got three symbols on the stack. What can I do with those you might ask, and that would be a nice segway into...

**Working with the stack**

_"A goal of a good Forth programmer is to minimize the use of these words, since they are only moving data around instead of doing useful work." - From the [The OLPC Wiki](http://wiki.laptop.org/go/Forth_stack_operators)_

`.` will drop one element from the stack.

    [any-word another-one and-then-another]
    $> .
    [any-word another-one]


`swap` will swap the two upmost elements on the stack.

    [any-word another-one]
    $> swap
    [another-one any-word]


`dup` will duplicate the topmost element on the stack (TOS)

    [another-one any-word]
    $> dup
    [another-one any-word any-word]


`2dup` will duplicate the two topmost elements on the stack.

    [another-one any-word any-word]
    $> 2dup
    [another-one any-word any-word any-word any-word]

Did I mention that you can drop several elements at once?

    [another-one any-word any-word any-word any-word]
    $> ...
    [another-one any-word]

`over` will copy the second element on the stack.

    [another-one any-word]
    $> over
    [another-one any-word another-one]

`rot` will move the third element to the top of the stack.

    [another-one any-word another-one]
    $> rot
    [any-word another-one another-one]

`empty` will check if the stack is empty, and push the result onto the stack as a [Church encoded boolean](./Stck/stdlib.md#booleans).

    [any-word another-one another-one]
    $> empty
    [any-word another-one another-one [. swap]]
    $> clear
    []
    $> empty
    [[.]]

In addition, the [Forth documentation](http://wiki.laptop.org/go/Forth_stack_operators) has a good description of different stack operators, along with reference implementations for less basic operators.

**Math**

_"For every epsilon>0 there is a delta>0 such that whenever |x-x_0|<delta, then |f(x)-f(x_0)|<epsilon." - From [Wikipedia](https://en.wikipedia.org/wiki/(%CE%B5,_%CE%B4)-definition_of_limit)_

All numerals are Church encoded and the following operators are supported: `+` (addition), `-` (subtraction), `*` and `%` (modulo). All operators perform on the two upmost elements on the stack, and push the result back on the stack as one number.

    []
    $> 1 1 +
    [2]

Numerals and booleans are "unchurched" for readability. This can be turned off with the command `church`.

    [2]
    $> church
    [[app || << rot rot << swap rot dup rot [app [app || swap rot dup swap [.]]] [app [app || swap rot dup swap [.]]]]]

You can turn on unchurching again with the command `unchurch`. Reading [stdlib.md](./Stck/stdlib.md) might help if that didn't make a lot of sense.

In addition the following predicates are supported for comparing numbers: `is-zero`, `<=` (less or equal), `>=` (greater or equal) and `=` (equal).

**Booleans**

Booleans are also Church encoded, and again [stdlib.md](./Stck/stdlib.md) is your friend. `true` is true and `false`is false.

The following boolean operators are supported: `not` (not), `and` (and), `or` (or), `<-` (left implication), `->` (right implication) and `<->` (equivalence).

**Anonymous stacks (Quotations)**

_"Anonymous stacks gives STCK the power of lambda functions™" - Anonymous STCK developer_

Chunks of STCK code can be pushed to the stack inside of anonymous stacks.

    []
    $> [false not]
    [[not false]]

Note how `false` and `not` wasn't executed. At a later time, the contents of an anonymous stack can be applied to the stack using `app`.

    [[not false]]
    $> app
    [true]

Anonymous stacks can be nested.

    $> [this [is [nested]]]
    [[[[nested] is] this]]
    $> app
    [this [[nested] is]]
    $> app
    [this is [nested]]
    $> app
    [this is nested]

A different set of stack operators work on anonymous stacks.

`||` (concat) will concatenate two anonymous stacks.

    $> [a b] [c d] ||
    [[d c b a]]

`|` (chop) will chop off the first element of the anonymous stack into a new anonymous stack.

    $> [a b c] |
    [[c b] [a]]

`<<` (ontop) pushes an element to the front of another anonymous stack.

    $> [last] first <<
    [[last first]]

`>>` (ontail) pushes an element to the end of another anonymous stack.

    $> [last] first >>
    [[first last]]

**Conditionals**

Conditionals follow the good old then-else-if construct: `<a boolean> [this anonymous stack will be applied if true] [this anonymous stack will be applied if false] ?`

    []
    $> true [this anonymous stack will be applied if true] [this anonymous stack will be applied if false] ?
    [this anonymous stack will be applied if [.]]

    []
    $> false [hot] [or-not] ?
    [or-not]

To be precise `?` is a subroutine that pops three elements from the stack, considers the last one to be a boolean, and applies either one or the other of the arguments.

**Subroutines**

_"What is a definition? Well classically a definition was colon something, and words, and end of definition somewhere." - [Chuck Moore](http://www.ultratechnology.com/1xforth.htm)_

Subroutines are declared by using `#`:

    [5 +] add-five #
    $> 2 add-five
    [7]

**Comments**

_"Due to INTERCAL's implementation of comment lines, most error messages are produced during execution instead of during compilation." - From the [INTERCAL Programming Language Revised Reference Manual](http://www.muppetlabs.com/~breadbox/intercal-man/s09.html)_

` ``` ` indicates the start and end of a comment. This is useful, as it enables STCK-programs to be embedded in markdown files.

    []
    $> ```This is a comment!```
    []

**Utility functions**

The interpreter defines a couple of utility functions, not strictly part of the STCK language.

`hprint` prints the content of the heap. This will list all declared subroutines.

`quit` exits the interpreter.

`load` loads external STCK-programs.


Projects using STCK
-------------------

**[hyperstck](https://github.com/einarwh/hyperstck)** - _A hypermedia-driven evaluator for the Stck programming language_

**[REST-STCK](https://github.com/teodoran/rest-stck)** - _Arbitrary Computation as a Service (ACaaS)_
