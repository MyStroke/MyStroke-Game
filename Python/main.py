import zmq
import numpy as np
from keras.models import load_model
import keras 


@keras.saving.register_keras_serializable('mystroke_classification_224.keras')
class MyModel(keras.layers.Dense):
  pass

assert keras.saving.get_registered_object('mystroke_classification_224.keras>MyModel') == MyModel
assert keras.saving.get_registered_name(MyModel) == 'mystroke_classification_224.keras>MyModel'

context = zmq.Context()
socket = context.socket(zmq.REP)
socket.bind("tcp://*:5555")
print('socket binded at port tcp 5555')
while True:
    bytes_received = socket.recv(3136)
    array_received = np.frombuffer(bytes_received, dtype=np.float32).reshape(28,28)
    pred = MyModel.predict(np.expand_dims(array_received.reshape(224,224), axis=0))
    print(pred)
    bytes_to_send = pred.tobytes()
    socket.send(bytes_to_send)