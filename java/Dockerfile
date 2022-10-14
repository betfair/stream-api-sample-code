FROM maven:3.8.5-openjdk-17 AS dependencies
WORKDIR /build
COPY java/console/pom.xml console/pom.xml
COPY java/client/pom.xml client/pom.xml
COPY java/swagger/pom.xml swagger/pom.xml
COPY java/pom.xml .
RUN mvn -B -e -C org.apache.maven.plugins:maven-dependency-plugin:3.1.2:go-offline
# Copy the dependencies from the dependencies stage
FROM maven:3.8.5-openjdk-17 as builder
WORKDIR /build
COPY --from=dependencies /root/.m2 /root/.m2
COPY --from=dependencies /build/ /build
COPY java/client/src /build/client/src
COPY java/console/src /build/console/src
COPY ESASwaggerSchema.json ../../ESASwaggerSchema.json
RUN mvn -B -e clean install -DskipTests=true
# At this point, BUILDER stage should have .jar file
FROM openjdk:17-jdk-slim
WORKDIR /build
COPY --from=builder /build/console/target/console-2.0.0.jar app.jar
CMD [ "java", "-jar", "/build/app.jar"]
