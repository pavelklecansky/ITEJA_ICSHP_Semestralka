var numberOfSides = 6
var sideLenght = 70
var angle = 360.0 / numberOfSides
var x = 0
 
while(x < sideLenght){
    Turtle.forward(numberOfSides)
    Turtle.right(angle)
    x = x + 1
}    
Turtle.done()