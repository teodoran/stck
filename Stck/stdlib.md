The STCK 2.0 Standard Library
=============================

Loading the standard library:
#load Stck/stdlib.stck

Derived stack operators
-----------------------
rot -> ```[[] swap << swap << swap >> app] rot #```

over -> ```[swap dup rot rot] over #```

2dup -> ```[over over] 2dup #```

Booleans
--------
Booleans uses the classic lambda calculus encoding

TRUE = λa . λb . a

FALSE = λa . λb . b

true -> ```[[.]] true #```

$> foo bar true app
[foo]

false -> ```[[swap .]] false #```

$> foo bar false app
[bar]

From there we can define boolean operators

IF = λp . λt . λe . p t e

if -> ```[rot app app] ? #```

$> true [foo] [bar] ?
[foo]

$> false [foo] [bar] ?
[bar]

not -> ```[[false] [true] ?] not #```

$> true not [foo] [bar] ?
[bar]

$> false not [foo] [bar] ?
[foo]

$> foo bar true not app
[bar]

$> foo bar false not app
[foo]

and -> ```[[[true] [false] ?] swap << [false] ?] and #```

$> foo bar false true and app
[bar]

$> foo bar true true and app
[foo]

$> true false and [foo] [bar] ?
[bar]

$> true true and [foo] [bar] ?
[foo]

or -> ```[not swap not and not] or #```

$> foo bar true false or app
[foo]

$> foo bar false false or app
[bar]

$> true false or [foo] [bar] ?
[foo]

$> false false or [foo] [bar] ?
[bar]

Implication and equivalence

```[not or] <- #```

```[swap <-] -> #```

```[2dup -> rot rot <- and] <-> #```

Math operations
---------------

INCREMENT = (n) -> (f) -> (x) -> f(n(f)(x))

Man ønsker å konstruere noe som lager [huh] [noe] -> (inc_resultat) app -> [huh] [noe] app [huh] app
inc -> ```[[swap dup rot swap >> [app] ||] swap << [app] ||] inc #```

### Some numbers
0 -> ```[[.]] 0 #```
1 -> ```[0 inc] 1 #```
2 -> ```[1 inc] 2 #```
3 -> ```[2 inc] 3 #```

Man ønsker å konstruere noe som lager [huh] 2 3 * -> (2*3) app -> [[huh] 2 app] 3 app
multiplication -> ```[[swap rot swap [app] swap << swap << swap app] swap << swap <<] * #```

Man ønsker å konstruere noe som lager [huh] 2 3 + -> (2+3) app -> [huh] 2 app [huh] 3 app
addition -> ```[[app] swap << swap [app] swap << [rot dup rot swap << rot rot << || app] swap << swap <<] + #```

### Note!

Enkod tall som par, og velg det andre når du skal ha et tall som er en mindre.


Still not organized operators
-----------------------------

equal -> ```[eq app] = #```

error -> ```[err app] error #```

empty -> ```[dup error] empty #```

clear -> ```[empty [] [. clear] ?] clear #```