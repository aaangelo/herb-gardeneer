
class Herb:
#plant has a type, knows if is wet or not, can die and has leaves.
    def __init__(self, type = 'basil', is_wet = False, is_alive = True, leaves = 1):
        self.type = type
        self.is_wet = is_wet
        self.is_alive = is_alive
        self.leaves = leaves
        self.waterd_timer = 0

    def __repr__(self):
        description = 'This herb is a {type}, it has {leaves} leaves'.format(type=self.type, leaves=self.leaves)
        if self.is_wet == False:
            description += ', is dry'
        else:
            description += ', is wet'
        if self.is_alive == False:
            description += ' and is dead.'
        else:
            description += ' and is alive.'
        return description
# methods will be to grow leaves when watered, to have leaves picked, and to die. maybe it can flower which gives you a new plant. 

    def grow(self):
        if self.is_alive == False:
            print('this plant is dead')
        elif self.is_wet == True:
            self.leaves += 1
            self.waterd_timer += 1
            self.is_wet = False
        return self.leaves

    def die(self):
        if self.is_alive == True:
            self.is_alive = False
        else:
            pass

    def over_water(self):
        if self.waterd_timer >= 3 and self.waterd_timer <=4 :
            print('dont just water this plant or it will die, try doing something else!')
        elif self.waterd_timer == 5:
            self.die()
            print('you over waterd this plant and it had died')
        else:
            pass




class Player:
    #player will have a name, a list of plants, and an amount of leaves.
    def __init__(self, plants_list, name = 'Tamzin', leaves = 0):
        self.name = name
        self.plants = plants_list
        self.current_plant = 0
        self.you_leaves = leaves

    def __repr__(self):
        print('Hi {name}, you have a:'.format(name=self.name))
        for plant in self.plants:
            print(plant.type)
        return 'and a total of {leaves} leaves'.format(leaves=self.you_leaves)
    #methods will be to water the plant, and to pick the leaves. maybe to get new plants too.

    def water(self):
        plant = self.plants[self.current_plant]
        if plant.is_alive == False:
            print('this plant is dead.')
        elif plant.is_wet == True:
            print('this plant is already wet')
        else:
            plant.is_wet = True
            plant.over_water()
            plant.grow()
            if plant.is_alive == False:
                pass
            else:
             print('you waterd this plant, it now has {leaves} leaves'.format(leaves=plant.leaves))

    def pick(self):
        plant = self.plants[self.current_plant]
        if plant.leaves == 0:
            plant.die()
            print ('oh bugger you picked all the leaves and killed it')
        elif plant.leaves >= 1 and plant.is_alive != False:
            plant.leaves -= 1
            plant.waterd_timer -=2
            self.you_leaves += 1
            print('you picked a leaf, you now have {leaves}'.format(leaves=self.you_leaves))
            if plant.leaves == 1:
                print('careful only 1 leaf left')
        else:
            'this plant is dead'
        
    def swap(self):
        self.current_plant += 1
        if self.current_plant >= len(self.plants):
            self.current_plant = 0
        print('you are now looking at your ' + str(self.plants[self.current_plant].type))

    def choice(self):
        go = input('what you you like to do? ')
        if go == 'water':
            me.water()
        elif go == 'pick':
            me.pick()
        elif go == 'swap':
            me.swap()
        else:
            print('problem')


            
            

list = []

print(""""
  _    _ ______ _____  ____                                
 | |  | |  ____|  __ \|  _ \                               
 | |__| | |__  | |__) | |_) |                              
 |  __  |  __| |  _  /|  _ <                               
 | |  | | |____| | \ \| |_) |                              
 |_|__|_|______|_|__\_\____/__  _   _ ______ ______ _____  
  / ____|   /\   |  __ \|  __ \| \ | |  ____|  ____|  __ \ 
 | |  __   /  \  | |__) | |  | |  \| | |__  | |__  | |__) |
 | | |_ | / /\ \ |  _  /| |  | | . ` |  __| |  __| |  _  / 
 | |__| |/ ____ \| | \ \| |__| | |\  | |____| |____| | \ \ 
  \_____/_/    \_\_|  \_\_____/|_| \_|______|______|_|  \_\                                                                       
  
  
 """)

namez = input('what is your name? ')
herb1 = Herb(input('what kind of herb would you like? '))
list.append(herb1)
herb2 = Herb(input('what 2nd herb herb would you like? '))
list.append(herb2)

me = Player(list, namez)
print(me)

print('in this game you can water you plants, pick the leaves, and swap which one your currently looking at!')
print('just type "water", "pick" or "swap"')
print('have fun')


goes = 0

while goes <= 100:
    me.choice()
    goes +=1



