STCK 2.0 Operator Guide
=======================

Lambda application
------------------
(AnonymousStack)

app -> [[a b c]] -> [a b c] (apply)

Stack Operators
---------------

drop -> .
(len -> len)
dup -> dup
swap -> swap
tail -> tail

<< -> [a [b] c] -> [a [c b]] (ontop)
>> -> [a [b] c] -> [a [b c]] (ontail)

[[] swap << swap << swap >> app] rot #
[swap dup rot rot] over #
[over over] 2dup #

Word definition
---------------

[anon stack] name #

Conditionals and Booleans
-------------------------

TRUE = λa . λb . a
[[app drop]] true #

FALSE = λa . λb . b
[[app swap drop]] false #

IF = λp . λt . λe . p t e
[[] swap << swap << swap app app] if #

[[false] [true] if] not #
[[[true] [false] if] swap << [false] if] and #

[not swap not and not] or #
[not or] <- #
[swap <-] -> #
[2dup -> rot rot <- and] <-> #

Missing primitives
------------------

= -> symbol equality
err -> is error
throw -> UserDefineException throw

empty -> [dup err [. true] [. false] ?]

Math Operators
--------------

Subject to change after encoding numbers

+ -> add
- -> substract
* -> multiply
i/ -> divide
% -> modulo
= -> equal
> -> greater
< -> less

rem -> dup rot swap 2dup i/ * - 1000000 * swap i/
/ -> 2dup i/ rot rot rem

Utility Operators
-----------------

sprint -> sprint (not core)
quit -> exit 0 (not core)

[len 0 =] empty #
[empty [] [. clear] ?] clear #
[len 1 = not [2dup > [swap .] [.] ? max] [] ?] max #
[len 1 = not [2dup < [swap .] [.] ? min] [] ?] min #
