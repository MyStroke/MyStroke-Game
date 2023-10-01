import zmq
import numpy as np
from keras.models import load_model
from PIL import Image
import keras 

# Model = load_model("mystroke_classification_224.keras")

context = zmq.Context()
socket = context.socket(zmq.REP)
socket.bind("tcp://*:5555")
print('socket binded at port tcp 5555')

i = 1
while True:   
    bytes_received = socket.recv(3686400)
    array_received = np.frombuffer(bytes_received, dtype=np.float32).reshape(720,1280)
    # array_received = np.asarray(bytearray(bytes_received), dtype=np.uint8)
    im = Image.fromarray(array_received).convert("RGB")
    im.save("your_file.jpeg")
    # pred = Model.predict(np.expand_dims(array_received.reshape(224,224), axis=0))
    print(array_received)
    bytes_to_send = pred.tobytes()
    socket.send(bytes_to_send)