import pygame

pygame.init()
display = pygame.display.set_mode((800, 600))

player=pygame.image.load("spaceship.png")
playerX, playerY, playerDx , playerDy = 400, 550, 0, 0

running = True

while running:
    for event in pygame.event.get():
        if event.type == pygame.QUIT:
            running = False

        if event.type == pygame.KEYDOWN:
            if event.key == pygame.K_LEFT:                playerDx = -0.1
            if event.key == pygame.K_RIGHT:             playerDx = 0.1
        if event.type == pygame.KEYUP:
            if event.key == pygame.K_RIGHT or event.key == pygame.K_LEFT:
                playerDx = 0

    playerX += playerDx
    display.fill((0, 0, 0))
    display.blit(player, (playerX, playerY))
    pygame.display.update()

pygame.quit()