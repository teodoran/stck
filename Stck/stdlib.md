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

True = λa . λb . a
False = λa . λb . b

true -> ```[[.]] true #```
false -> ```[[swap .]] false #```

From there we can define boolean operators

IF = λp . λt . λe . p t e

if -> ```[rot app app] ? #```

not -> ```[[false] [true] ?] not #```

and -> ```[[[true] [false] ?] swap << [false] ?] and #```

or -> ```[not swap not and not] or #```

Implication and equivalence

```[not or] <- #```

```[swap <-] -> #```

```[2dup -> rot rot <- and] <-> #```

Math operations
---------------

Successor = (n) -> (f) -> (x) -> f(n(f)(x))

Man ønsker å konstruere noe som lager [huh] [noe] -> (succ_resultat) app -> [huh] [noe] app [huh] app
succ -> ```[| [swap dup rot swap ||] rot || ||] succ #```

### Some numbers
0 -> ```[[[.] app]] 0 #```
1 -> ```[0 succ] 1 #```
2 -> ```[1 succ] 2 #```
3 -> ```[2 succ] 3 #```
4 -> ```[3 succ] 4 #```
5 -> ```[4 succ] 5 #```
6 -> ```[5 succ] 6 #```
7 -> ```[6 succ] 7 #```
8 -> ```[7 succ] 8 #```
9 -> ```[8 succ] 9 #```
10 -> ```[9 succ] 10 #```
100 -> ```[10 10 *] 100 #```
1000 -> ```[100 10 *] 1000 #```
1M -> ```[1000 1000 *] 1M #```

Man ønsker å konstruere noe som lager [huh] 2 3 * -> (2*3) app -> [[huh] 2 app] 3 app
multiplication -> ```[[swap rot swap [app] swap << swap << swap app] swap << swap <<] * #```

Man ønsker å konstruere noe som lager [huh] 2 3 + -> (2+3) app -> [huh] 2 app [huh] 3 app
addition -> ```[[app] swap << swap [app] swap << [rot dup rot swap << rot rot << || app] swap << swap <<] + #```

### Predecessor
pred-first -> ```[0 false] pred-first #```


Hvordan skal pred-next fungere?
pred-first: 0 false
pred-first pred-next: 0 true
pred-first pred-next pred-next: 1 true
pred-first pred-next pred-next pred-next: 2 true
pred-first pred-next pred-next pred-next pred-next: 3 true
pred-next -> ```[[succ true] [true] ?] pred-next #```

pred -> ```[pred-first rot [pred-next] swap app .] pred #```

x [f] pred-first pred-next 3 app . -> pred-first pred-next pred-next pred-next -> 2 true . -> 2

Man ønsker å konstruere noe som lager [huh] 3 2 - -> (3-2) app -> [huh] 2 app [huh] 3 app
substraction -> ```[[pred] swap app] - #```

### Predicates
is-zero -> ```[true [. false] rot app] is-zero #```
less-or-equal -> ```[swap - is-zero] <= #```
greater-or-equal -> ```[- is-zero] >= #```
equal -> ```[2dup >= rot rot <= and] = #```

### remainder/modulo (%)

remainder -> ```[2dup <= [dup rot swap - swap %] [.] ?] % #```


### Note!

Enkod tall som par, og velg det andre når du skal ha et tall som er en mindre.


Still not organized operators
-----------------------------

error -> ```[err app] error #```

empty -> ```[dup error] empty #``` TODO: Empty is buggy

clear -> ```[empty [] [. clear] ?] clear #```