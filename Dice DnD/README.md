Report o DnD kostce

V mém programu na Arduino DnD kostku používám třídy Digit, Display, Button, Dice
Digit je pomocná třída pro Display a zajišťuje, že vím na jaké pozici se má nacházet číslo vypisované v displeji, tuto třídu mám kvůli multiplexingu displeje.
Display je třída která zajišťuje vypsání vstupu na displej, má 2 funkce, podle toho jestli se nacházíme v konfiguračním nebo normálním modu, jelikož vypsání v konfiguračním módu je náročnější tím, že vstupem není jen číslo ale 2 nesouvisející čísla a písmeno 'd'.