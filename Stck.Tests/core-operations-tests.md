Core Operations Tests
=====================

empty program
-------------

```[] [] eq```

stack operations
----------------

### push symbols
```
[my-symbol] [my-symbol] eq 
[first next last] [first next last] eq 
```

### drop (.)
```
[not-empty .] [] eq 
[.] [STACK_UNDERFLOW throw] eq 
[keep-this drop-this and-drop-this . . drop-this .] [keep-this] eq 
[..] [] eq 
```

### dup
```
[duplicated dup] [duplicated duplicated] eq 
[dup] [STACK_UNDERFLOW throw] eq 
```

### swap
```
[last first swap] [first last] eq 
[one swap] [one STACK_UNDERFLOW throw] eq 
```

### emp
```
[emp] [[true]] eq 
[something emp] [something [false]] eq 
```

anonymous stacks and their operations
-------------------------------------

### anonymous stacks / quotations ([])
```
[[anonymous stack]] [[anonymous stack]] eq 
[[anonymous [nested] stack]] [[anonymous [nested] stack]] eq 
[first [next last] app] [first next last] eq 
[a-word app] [a-word MISSING_QUOTATION throw] eq 
```

### concat (||)
```
[[a b] [c d] ||] [[a b c d]] eq 
[[one] ||] [[one] STACK_UNDERFLOW throw] eq 
```

### chop (|)
```
[[a b c] |] [[b c] [a]] eq 
[[] |] [[] []] eq 
[one |] [one STACK_UNDERFLOW throw] eq 
```

### ontail (>>)
```
[[last] first >>] [[last first]] eq 
[one >>] [one STACK_UNDERFLOW throw] eq 
```

### ontop (<<)
```
[[last] first <<] [[first last]] eq 
[one <<] [one STACK_UNDERFLOW throw] eq 
```

### definitions (#)
```
[[word-content] my-word #] [] eq 
[[word-content] my-word # my-word] [word-content] eq 
[not-quotation my-word #] [not-quotation my-word STACK_UNDERFLOW throw] eq 
```

### exceptions
```
[fail throw] [fail throw] eq 
[throw] [STACK_UNDERFLOW throw] eq 
[fail throw err] [[true]] eq 
[not-exception: err] [[false]] eq 
[err] [STACK_UNDERFLOW throw] eq 
```

### comments (tests disabled)
_Moving to markdown makes testing comments hard..._

`[` ` `This is a comment` ` `] [] eq`
`[first ` ` `should not be affected` ` ` last] [first last] eq`

### eq
```
[a] [a] eq 
[a b] [a b] eq 
[a b [c d e]] [a b [c d e]] eq 
[a b swap] [b a] eq 
[[last] first >>] [[last first]] eq 
```
