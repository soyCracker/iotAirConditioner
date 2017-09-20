### raspberry pi3 lirc紅外線遙控套件設定教學： 
安裝：
`````
sudo apt-get install lirc
`````
配置：
`````
#定義GPIO 18來接收訊號，GPIO 17來發射訊號
sudo nano /boot/config.txt 
#在文件结尾添加
dtoverlay=lirc-rpi
gpio_in_pin=18
gpio_out_pin=17

#sudo  nano /etc/modules #添加下面兩行
lirc_dev
lirc_rpi gpio_in_pin=18 gpio_out_pin=17

#sudo nano /etc/lirc/hardware.conf  #編輯LRIC的配置文件
LIRCD_ARGS="--uinput --listen"
DRIVER="default"
DEVICE="/dev/lirc0"
MODULES="lirc_rpi"

# 重啟生效(若出現任何錯誤，而無法使用紅外線接收器也可以用)
sudo /etc/init.d/lirc restart 或是 sudo reboot

# 測試紅外線接收器IR receiver是否正常運作，請執行
mode2 -d /dev/lirc0
`````
錄製：
`````
# 雖然我是要遙控冷氣，但我是用錄製電風扇的方法，反正我只是要開關而已，要看正確錄製冷氣方法參考底下網址
# 開始錄製
irrecord -d /dev/lirc0 ~/lircd.conf #按照提示操作即可,錄製完後會讓你輸入按鍵名
sudo cp ~/lircd.conf /etc/lirc/lircd.conf

# 查看錄製好可以使用的按键名
irsend LIST /home/pi/lircd.conf ""
# 發射KEY_POWER紅外線
irsend SEND_ONCE /home/pi/lircd.conf KEY_POWER
`````
參考網址：  
http://blog.just4fun.site/raspberrypi-lirc.html  
http://raspberrytw.tumblr.com/post/48939062042/raspberry-pi-lirc-implement
