# PIPTasks
TODO:

1)Card arrangement and sorting:
 
 a)Load information about cards from a json file.
 
 b)Arrange cards horizontally.
 
 c)Users should be able to select one or more cards & make a group out of it.
 
 d)Ability to drag & drop a card from one group to another.
 
 e)If the number of cards in a group is zero, delete that group.
 
 
2)Popup system:
 
 a)Ability to show one popup on top of another. Should work like a stack. Most recent one should be on top.
 
 b)Only one popup to be visible at a time.
 
 c)All popups to have a back button on top
 
 d)All popups have to have a header.
 
 e)Each popup to have ability to accept anydata type & show in UI accordingly. Ex a json structure, List, int etc
 
 f)Functionality to remove all popups at once. Ex: lets say we push 3 Popups one after another, we should be able to remove all popups in the list


3)Keyboard splitting:
 
 a)Evaluate the possibility of having split screen keyboard.
 
 b)We should be able to see the text as we start typing.
 
 c)Evaluate other options (if any)
 

4)Highlight multiple UI items:
 
 a)This is in the context of tutorials. We need the ability to highlight 1 or more UI items. We need functionality to highlight multiple items.
 
 b)The Canvas & highlighting system to be on separate screens (Canvas), Item(s) to be highlighted must be dynamic.


5)User Inventory:
 
 a)Create a PlayerData class that holds user inventory such as purchased items
 
 b)Load & save PlayerData in PlayerPrefs
 
 c)Create a StoreData class that has ‘n’ no of items loaded from a json file store_data.json.
 
 d)Show the items loaded from store_data.json in store UI.
 
 e)Once a user purchases, add it to user inventory.
 
 f)The store item once purchased, should not be shown again.



