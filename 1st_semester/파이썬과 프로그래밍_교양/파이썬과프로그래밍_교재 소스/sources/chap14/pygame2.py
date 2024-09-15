import pygame

pygame.init()
display = pygame.display.set_mode((800, 600))
myfont = pygame.font.SysFont('Comic Sans MS', 30)
score = 0
player=pygame.image.load("spaceship.png")
playerX, playerY, playerDx , playerDy = 400, 550, 0, 0

running = True
while running:
    for event in pygame.event.get():
        if event.type == pygame.QUIT:
            running = False

    display.fill((0, 0, 0))
    display.blit(player, (playerX, playerY))
    pygame.display.update()

pygame.quit()
