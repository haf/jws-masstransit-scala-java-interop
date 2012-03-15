# 2012-03-15 hack-evening

See http://readthedocs.org/docs/masstransit/en/latest/advanced/interop.html?highlight=rfc

AMQP 0.9

`amqp://isomorphism`

Send to exchange: `Jayway.Test.Receiver`

Sample JSON:

```
 {
  "destinationAddress": "rabbitmq://isomorphism/Jayway.Test.Receiver",
  "headers": {},
  "message": {
    "spoken": "Something wierd is going on!",
    "seqId": 2
  },
  "messageType": [
    "urn:message:Jayway.Test.Messages.DynamicImpl:ChatMessage",
    "urn:message:Jayway.Test.Messages:ChatMessage"
  ],
  "retryCount": 0
}
```