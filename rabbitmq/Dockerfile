FROM rabbitmq:3-management

RUN rabbitmq-plugins enable --offline rabbitmq_shovel && rabbitmq-plugins enable --offline rabbitmq_shovel_management

EXPOSE 15672
EXPOSE 5672