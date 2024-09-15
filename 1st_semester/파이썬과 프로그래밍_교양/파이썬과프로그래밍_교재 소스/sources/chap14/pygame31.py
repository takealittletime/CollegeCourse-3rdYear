import pygame

pygame.init()
display = pygame.display.set_mode((800, 600))

player=pygame.image.load("spaceship.png")
playerX, playerY, playerDx , playerDy = 400, 550, 0, 0

alien=pygame.image.load("alien.png")
alienX, alienY, alienDx , alienDy   = 0, 10, 0.1, 0.1

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

    alienX += alienDx
    if  alienX <= 0 or alienX > 750:
        alienDx *= -1
        alienY += 30
    display.fill((0, 0, 0))
    display.blit(player, (playerX, playerY))
    display.blit(alien, (alienX, alienY))
    pygame.display.update()

pygame.quit()