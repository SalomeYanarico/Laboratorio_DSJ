import pygame
import math
import random
from pygame import mixer

# Initialize the pygame
pygame.init()

# Create the screen
screen = pygame.display.set_mode((800, 600))

# Background
background = pygame.image.load('background.jpeg')
background = pygame.transform.scale(background, (800, 600))

# Background Sound
mixer.music.load('background.wav')
mixer.music.play(-1)

# Title and icon
pygame.display.set_caption("Space Invaders")
icon = pygame.image.load('ufo.png')
pygame.display.set_icon(icon)

# Player
playerImg = pygame.image.load('player.png')
playerImg = pygame.transform.scale(playerImg, (50, 50))
playerX = 370
playerY = 480
playerX_change = 0
playerY_change = 0

# Player Life
player_life = 4
max_life = player_life

# Enemy
enemyImg = []
enemyX = []
enemyY = []
enemyX_change = []
enemyY_change = []
num_of_enemies = 6

for i in range(num_of_enemies):
    enemyImg.append(pygame.image.load('enemy.png'))
    enemyImg[i] = pygame.transform.scale(enemyImg[i], (40, 40))  # Disminuir el tamaño de los enemigos
    enemyX.append(random.randint(0, 735))
    enemyY.append(random.randint(50, 150))
    enemyX_change.append(random.choice([-0.5, 0.5]))
    enemyY_change.append(random.choice([0.1, 0.2]))

# Bullet
bulletImg = pygame.image.load('bullet.png')
bulletImg = pygame.transform.scale(bulletImg, (20, 20))  # Disminuir el tamaño de la bala
bulletX = 0
bulletY = 480
bulletX_change = 0
bulletY_change = 5
bullet_state = "ready"

# Score
score_value = 0
font = pygame.font.Font('freesansbold.ttf', 32)
textX = 10
textY = 10

# Game Over text
over_font = pygame.font.Font('freesansbold.ttf', 64)
def game_over_text():
    over_text = over_font.render("GAME OVER", True, (0, 255, 0))
    screen.blit(over_text, (250, 250))

# Barra de vida
def draw_life_bar(x, y, life, max_life):
    life_ratio = life / max_life
    pygame.draw.rect(screen, (255, 0, 0), (x, y, 200, 20))  # Fondo rojo de la barra
    pygame.draw.rect(screen, (0, 255, 0), (x, y, 200 * life_ratio, 20))  # Barra verde sobre la vida restante


def show_score(x, y):
    score = font.render("Score: " + str(score_value), True, (0, 255, 0))
    screen.blit(score, (x, y))


def player(x, y):
    screen.blit(playerImg, (x, y))


def enemy(x, y, i):
    screen.blit(enemyImg[i], (x, y))


def fire_bullet(x, y):
    global bullet_state
    bullet_state = "fire"
    screen.blit(bulletImg, (x + 16, y + 10))


def isCollision(objX, objY, bulletX, bulletY, threshold=27):
    distance = math.sqrt((math.pow(objX - bulletX, 2)) + (math.pow(objY - bulletY, 2)))
    return distance < threshold


# Game Loop
running = True
game_over = False
played_gameover_sound = False

while running:

    screen.fill((0, 0, 0))


    screen.blit(background, (0, 0))

    for event in pygame.event.get():
        if event.type == pygame.QUIT:
            running = False


        if event.type == pygame.KEYDOWN and not game_over:
            if event.key == pygame.K_LEFT:
                playerX_change = -0.5
            if event.key == pygame.K_RIGHT:
                playerX_change = 0.5
            if event.key == pygame.K_UP:
                playerY_change = -0.5
            if event.key == pygame.K_DOWN:
                playerY_change = 0.5
            if event.key == pygame.K_SPACE:
                if bullet_state == "ready":
                    bullet_Sound = mixer.Sound('laser.wav')
                    bullet_Sound.play()
                    bulletX = playerX
                    fire_bullet(bulletX, bulletY)

        if event.type == pygame.KEYUP:
            if event.key == pygame.K_LEFT or event.key == pygame.K_RIGHT:
                playerX_change = 0
            if event.key == pygame.K_UP or event.key == pygame.K_DOWN:
                playerY_change = 0

    if not game_over:
        playerX += playerX_change
        playerY += playerY_change

    if playerX <= 0:
        playerX = 0
    elif playerX >= 736:
        playerX = 736


    if playerY <= 0:
        playerY = 0
    elif playerY >= 536:
        playerY = 536


    for i in range(num_of_enemies):
        if not game_over:
            # Move enemy
            enemyX[i] += enemyX_change[i]
            enemyY[i] += enemyY_change[i]

            # Reverse enemy movement at boundaries
            if enemyX[i] <= 0 or enemyX[i] >= 736:
                enemyX_change[i] *= -1
            if enemyY[i] >= 600 or enemyY[i] <= 0:
                enemyY_change[i] *= -1

        # Check for collision with player (Life loss or Game Over)
        if isCollision(enemyX[i], enemyY[i], playerX, playerY, 40):
            if player_life > 0:
                player_life -= 1
                enemyX[i] = random.randint(0, 736)
                enemyY[i] = random.randint(50, 150)
            if player_life == 0 and not played_gameover_sound:
                mixer.music.stop()
                gameover_Sound = mixer.Sound('gameover.wav')
                gameover_Sound.play()
                game_over = True
                played_gameover_sound = True
            break

        if not game_over:
            # Collision with bullet
            collision = isCollision(enemyX[i], enemyY[i], bulletX, bulletY)
            if collision:
                explosion_Sound = mixer.Sound('explosion.wav')
                explosion_Sound.play()
                bulletY = 480
                bullet_state = "ready"
                score_value += 1
                enemyX[i] = random.randint(0, 736)
                enemyY[i] = random.randint(50, 150)

        enemy(enemyX[i], enemyY[i], i)


    if bulletY <= 0:
        bulletY = 480
        bullet_state = "ready"

    if bullet_state == "fire":
        fire_bullet(bulletX, bulletY)
        bulletY -= bulletY_change


    player(playerX, playerY)

    show_score(textX, textY)
    draw_life_bar(600, 10, player_life, max_life)

    if game_over:
        game_over_text()

    # Update display
    pygame.display.update()