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
false -> ```[[swap .]] false #```

From there we can define boolean operators

IF = λp . λt . λe . p t e

if -> ```[rot app app] ? #```

not -> ```[[false] [true] ?] not #```

and -> ```[[[true] [false] ?] swap << [false] ?] and #```

or -> ```[not swap not and not] or #```

Implication and equivalence

```[not or] <= #```

```[swap <=] => #```

```[2dup => rot rot <= and] <=> #```

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